using Microsoft.EntityFrameworkCore;
using Despesas_23504.Models;
using Despesas_23504.Data;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Configurações
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Middlewares
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Endpoints
app.MapPost("/despesas", async (AppDbContext context, Despesa despesa) =>
{
    try
    {
        context.Despesas.Add(despesa);
        await context.SaveChangesAsync();
        return Results.Created($"/despesas/{despesa.Id}", despesa);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

app.MapGet("/despesas", async (AppDbContext context) =>
{
    return await context.Despesas.ToListAsync();
});

app.MapPost("/popular-despesas", async (AppDbContext context) =>
{
    var despesas = new List<Despesa>
    {
        new() { Nome = "Mercado", Valor = 150.50m },
        new() { Nome = "Gasolina", Valor = 200.00m }
    };

    await context.Despesas.AddRangeAsync(despesas);
    await context.SaveChangesAsync();
    return Results.Ok("Dados iniciais criados!");
});

app.Run();