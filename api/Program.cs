using NetEscapades.Extensions.Logging.RollingFile;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using FinancialControllerServer.Application.Common;
using FinancialControllerServer.Application.Usuarios.CreateUsuario;
using FinancialControllerServer.Application.Common.Interfaces;
using FinancialControllerServer.Domain.Interfaces;
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

// Logging in files only on production environment.
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

app.MapControllers();
app.Run();
