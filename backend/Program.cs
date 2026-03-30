using System.Text;
using System.Text.Json;
using NetEscapades.Extensions.Logging.RollingFile;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FinancialControllerServer.Api.Middlewares;
using FinancialControllerServer.Application.Auth.Login;
using FinancialControllerServer.Application.Common.Cryptography;
using FinancialControllerServer.Application.Common.Interfaces;
using FinancialControllerServer.Application.Common.Auth;
using FinancialControllerServer.Application.Categorias.CreateCategoria;
using FinancialControllerServer.Application.Categorias.ListCategorias;
using FinancialControllerServer.Application.Usuarios.CreateUsuario;
using FinancialControllerServer.Application.Pessoas.CreatePessoa;
using FinancialControllerServer.Application.Pessoas.ListPessoas;
using FinancialControllerServer.Application.Pessoas.DeletePessoa;
using FinancialControllerServer.Application.Transacoes.CreateTransacao;
using FinancialControllerServer.Application.Transacoes.ListTransacoes;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;
using FinancialControllerServer.Infrastructure.Persistence;
using FinancialControllerServer.Infrastructure.Services;
using FinancialControllerServer.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuring Swagger documentation.
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Financial Controller API",
        Description = "API for Financial Controller features."
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// CORS Configuration
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
        else
        {
            policy
                .WithOrigins(allowedOrigins ?? Array.Empty<string>())
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

// Logging to files only in the production environment.
if (builder.Environment.IsProduction())
{
    string logDirectory = builder.Configuration["Logging:LogDirectory"] ?? "Logs";
    Directory.CreateDirectory(logDirectory);

    builder.Logging.AddFile(options =>
    {
        options.Periodicity = PeriodicityOptions.Daily;
        options.LogDirectory = logDirectory;
        options.FileName = "financial_controller-";
        options.Extension = ".log";
        options.RetainedFileCountLimit = 7;
    });
}

// Configuring Model Validadtion to use BadRequestException and GlobalExceptionHandlingMiddleware.
builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .SelectMany(x => x.Value!.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        throw new BadRequestException(
            "Erro de validação",
            errors
        );
    }
);

// Registering JWT configuration.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

// Setting JWT as the default authentication mechanism
var jwtSection = builder.Configuration.GetSection("Jwt");

var key = jwtSection["Key"]
    ?? throw new InvalidOperationException("JWT Key is missing");

var issuer = jwtSection["Issuer"]
    ?? throw new InvalidOperationException("JWT Issuer is missing");

var audience = jwtSection["Audience"]
    ?? throw new InvalidOperationException("JWT Audience is missing");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key))
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();

            var response = new ApiErrorResponse
            {
                Message = "Não autenticado",
                Errors = new List<string>(),
                TraceId = context.HttpContext.TraceIdentifier
            };

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        },

        OnForbidden = context =>
        {
            var response = new ApiErrorResponse
            {
                Message = "Acesso negado",
                Errors = new List<string>(),
                TraceId = context.HttpContext.TraceIdentifier
            };

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    };
});

// Infrastructure
builder.Services
    .AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services
    .Configure<PasswordHashOptions>(builder.Configuration.GetSection("PasswordHash"));

// Application
builder.Services
    .AddScoped<CreateUsuarioHandler>()
    .AddScoped<LoginHandler>()
    .AddScoped<CreateCategoriaHandler>()
    .AddScoped<ListCategoriasHandler>()
    .AddScoped<CreatePessoaHandler>()
    .AddScoped<ListPessoasHandler>()
    .AddScoped<DeletePessoaHandler>()
    .AddScoped<CreateTransacaoHandler>()
    .AddScoped<ListTransacoesHandler>()
    .AddScoped<ISenhaHasher, SenhaHasher>()
    .AddScoped<ITokenService, TokenService>()
    .AddScoped<ICategoriaRepository, CategoriaRepository>()
    .AddScoped<IUsuarioRepository, UsuarioRepository>()
    .AddScoped<IPessoaRepository, PessoaRepository>()
    .AddScoped<ITransacaoRepository, TransacaoRepository>();

var app = builder.Build();

// Enabling Swagger access only for Development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseCors("CorsPolicy");
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
