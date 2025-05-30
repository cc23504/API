using Microsoft.AspNetCore.Mvc;



var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ----------------------------------------
// Desenvolvimento da API
app.MapGet("/despesa/{codigo}", ([FromRoute] string codigo) => {
    var desp = RepositorioDeDespesa.PegarPorCodigo(codigo);
    return desp;
});

app.MapPost("/despesa", (Despesa desp) => {
    RepositorioDeDespesa.Adicionar(desp);
});

app.MapPut("/despesa", (Despesa desp) => {
    var despSalvo = RepositorioDeDespesa.PegarPorCodigo(desp.Codigo);
    if (despSalvo != null)
        despSalvo.Nome = desp.Nome;
});

app.MapDelete("/despesa/{codigo}", ([FromRoute] string codigo) => {
    var despSalvo = RepositorioDeDespesa.PegarPorCodigo(codigo);
    if (despSalvo != null)
        RepositorioDeDespesa.Remover(despSalvo);
    return Results.Ok();
});

// ----------------------------------------
app.Run();

// Classe Despesa
public class Despesa
{
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
}

public static class RepositorioDeDespesa
{
    public static List<Despesa>? Despesas { get; set; }

    public static void CriarBanco()
    {
        if (Despesas == null)
        {
            Despesas = new List<Despesa>();

            Despesa d = new Despesa { Codigo = "001", Nome = "Supermercado" };
            Despesas.Add(d);
            d = new Despesa { Codigo = "002", Nome = "Energia elétrica" };
            Despesas.Add(d);
            d = new Despesa { Codigo = "003", Nome = "Água" };
            Despesas.Add(d);
            d = new Despesa { Codigo = "004", Nome = "Transporte" };
            Despesas.Add(d);
        }
    }

    public static void Adicionar(Despesa desp)
    {
        if (Despesas == null) CriarBanco();
        Despesas.Add(desp);
    }

    public static Despesa PegarPorCodigo(string cod)
    {
        if (Despesas == null) 
            CriarBanco();
        return (Despesas ?? new List<Despesa>()).FirstOrDefault(d => d.Codigo == cod);

    }


    public static void Remover(Despesa desp)
    {
        Despesas?.Remove(desp);
    }
}
