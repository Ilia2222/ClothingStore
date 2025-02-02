using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;
using ClothingStore.Services;
using ClothingStore.Repositories;
using ClothingStore.Data;

var builder = WebApplication.CreateBuilder(args);

// Конфигуриране на Serilog за логване
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

//  Конфигуриране на базата данни (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистриране на репозиторита и услуги (Dependency Injection)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

//  Добавяне на FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddControllers().AddFluentValidation();

//  Добавяне на Swagger за документация
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавяне на Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Инициализация на SeedData (за добавяне на начални продукти)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    SeedData.Initialize(context); 
}

// Middleware за пренасочване към HTTPS
app.UseHttpsRedirection();

//  Включване на Swagger UI за среда на разработка
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//  Middleware за авторизация и маршрутизация
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");


app.Run();