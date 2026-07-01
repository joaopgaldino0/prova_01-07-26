using joaopedro.api.Data;
using joaopedro.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total", 
        configs => configs
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
);

var app = builder.Build();

List<Pressao> PressaoArterial = new List<Livro>();

app.MapPost("/api/pressao/cadastrar",
    ([FromBody] Pressao pressao,
    [FromServices] AppDataContext ctx) =>
{
    Pressao? resultado = ctx.PressaoArterial.FirstOrDefault
        (x => x.Nome == pressao.Nome);

    if (resultado is not null)
    {
        return Results.Conflict("Já existe!");
    }

    ctx.PressaoArterial.Add(pressao);
    ctx.SaveChanges();
    return Results.Created("", pressao);
});

app.MapGet("/api/pressao/listar",
    ([FromServices] AppDataContext ctx) =>
{
    if (ctx.PressaoArterial.Any())
    {
        return Results.Ok(ctx.PressaoArterial.ToList());
    }
    return Results.NotFound("Vazio!");
});

app.MapDelete("/api/pressao/alterar/{id}",
    ([FromRoute] string id,
    [FromServices] AppDataContext ctx) =>
{

    Pressao? resultado = ctx.PressaoArterial.Find(id);

    if (resultado is not null)
    {
        ctx.PressaoArterial.Remove(resultado);
        ctx.SaveChanges();
        return Results.NoContent();
    }

    return Results.NotFound("Não encontrado!");
});


app.UseCors("Acesso Total");

app.Run();