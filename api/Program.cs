using NetEscapades.Extensions.Logging.RollingFile;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FinancialControllerServer.Api.Middlewares;
using FinancialControllerServer.Application.Common;
using FinancialControllerServer.Application.Usuarios.CreateUsuario;
using FinancialControllerServer.Application.Common.Interfaces;
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

// Infrastructure
builder.Services
    .AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services
    .Configure<SecurityOptions>(builder.Configuration.GetSection("Security"));

// Application
builder.Services
    .AddScoped<CreateUsuarioHandler>()
    .AddScoped<ISenhaHasher, SenhaHasher>()
    .AddScoped<IUsuarioRepository, UsuarioRepository>();

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

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();
