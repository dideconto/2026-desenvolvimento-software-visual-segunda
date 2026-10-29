using Diogo.Data;
using Diogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDataContext ctx = scope.ServiceProvider.GetRequiredService<AppDataContext>();
    ctx.Database.Migrate();
}

app.MapGet("/", () => "API de Folha de Pagamento");

// POST: /api/folha/cadastrar
app.MapPost("/api/folha/cadastrar", ([FromBody] FolhaPagamento folha, [FromServices] AppDataContext ctx) =>
{
    if (folha.Mes < 1 || folha.Mes > 12 || folha.Ano < 2000)
    {
        return Results.BadRequest("Mês ou ano inválido.");
    }

    FolhaPagamento? folhaEncontrada = ctx.Folhas.FirstOrDefault(x =>
        x.Cpf == folha.Cpf && x.Mes == folha.Mes && x.Ano == folha.Ano);
    if (folhaEncontrada is not null)
    {
        return Results.BadRequest("Já existe uma folha para esse CPF, mês e ano.");
    }

    folha.SalarioBruto = folha.HorasTrabalhadas * folha.ValorHora;
    folha.ImpostoRenda = CalcularImpostoRenda(folha.SalarioBruto);
    folha.Inss = CalcularInss(folha.SalarioBruto);
    folha.Fgts = folha.SalarioBruto * 0.08;
    folha.SalarioLiquido = folha.SalarioBruto - folha.ImpostoRenda - folha.Inss;

    ctx.Folhas.Add(folha);
    ctx.SaveChanges();
    return Results.Created("", folha);
});

// GET: /api/folha/listar
app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) =>
{
    if (!ctx.Folhas.Any())
    {
        return Results.NotFound("Nenhuma folha cadastrada.");
    }

    return Results.Ok(ctx.Folhas.ToList());
});

// GET: /api/folha/buscar/cpf/mes/ano
app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}",
    ([FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano, [FromServices] AppDataContext ctx) =>
{
    FolhaPagamento? folha = ctx.Folhas.FirstOrDefault(x => x.Cpf == cpf && x.Mes == mes && x.Ano == ano);
    if (folha is null)
    {
        return Results.NotFound("Folha não encontrada.");
    }

    return Results.Ok(folha);
});

// DELETE: /api/folha/remover/cpf/mes/ano
app.MapDelete("/api/folha/remover/{cpf}/{mes}/{ano}",
    ([FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano, [FromServices] AppDataContext ctx) =>
{
    FolhaPagamento? folha = ctx.Folhas.FirstOrDefault(x => x.Cpf == cpf && x.Mes == mes && x.Ano == ano);
    if (folha is null)
    {
        return Results.NotFound("Folha não encontrada.");
    }

    ctx.Folhas.Remove(folha);
    ctx.SaveChanges();
    return Results.Ok(folha);
});

// GET: /api/folha/total-liquido
app.MapGet("/api/folha/total-liquido", ([FromServices] AppDataContext ctx) =>
{
    if (!ctx.Folhas.Any())
    {
        return Results.NotFound("Nenhuma folha cadastrada.");
    }

    double total = ctx.Folhas.ToList().Sum(x => x.SalarioLiquido);
    return Results.Ok(new { totalLiquido = total });
});

app.Run();

static double CalcularImpostoRenda(double salarioBruto)
{
    double aliquota;
    double parcelaDeduzir;

    if (salarioBruto <= 1903.98)
    {
        return 0;
    }
    else if (salarioBruto <= 2826.65)
    {
        aliquota = 0.075;
        parcelaDeduzir = 142.80;
    }
    else if (salarioBruto <= 3751.05)
    {
        aliquota = 0.15;
        parcelaDeduzir = 354.80;
    }
    else if (salarioBruto <= 4664.68)
    {
        aliquota = 0.225;
        parcelaDeduzir = 636.13;
    }
    else
    {
        aliquota = 0.275;
        parcelaDeduzir = 869.36;
    }

    return salarioBruto * aliquota - parcelaDeduzir;
}

static double CalcularInss(double salarioBruto)
{
    if (salarioBruto <= 1693.72)
    {
        return salarioBruto * 0.08;
    }
    if (salarioBruto <= 2822.90)
    {
        return salarioBruto * 0.09;
    }
    if (salarioBruto <= 5645.80)
    {
        return salarioBruto * 0.11;
    }
    return 621.03;
}
