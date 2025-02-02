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

// ������������� �� Serilog �� �������
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

//  ������������� �� ������ ����� (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ������������ �� ������������ � ������ (Dependency Injection)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

//  �������� �� FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddControllers().AddFluentValidation();

//  �������� �� Swagger �� ������������
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// �������� �� Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// ������������� �� SeedData (�� �������� �� ������� ��������)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    SeedData.Initialize(context); 
}

// Middleware �� ������������ ��� HTTPS
app.UseHttpsRedirection();

//  ��������� �� Swagger UI �� ����� �� ����������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//  Middleware �� ����������� � �������������
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");


app.Run();