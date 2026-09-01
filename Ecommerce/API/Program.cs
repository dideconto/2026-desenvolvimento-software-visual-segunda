//POSTMAN
//INSONMIA
//REST CLIENT - Extensão do VSCODE

//TERMINAL
//1 - Criar solução
//2 - Entrar na pasta da solução
//3 - Criar o projeto
//4 - Vincular o projeto para a solução
Console.Clear();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Produto> produtos = new List<Produto>
{
    new Produto
    {
        Nome = "Notebook Pro 15"
    },
    new Produto
    {
        Nome = "Mouse Wireless"
    },
    new Produto
    {
        Nome = "Teclado Mecânico RGB"
    },
    new Produto
    {
        Nome = "Monitor 27 Full HD"
    },
    new Produto
    {
        Nome = "Headset Gamer"
    },
    new Produto
    {
        Nome = "Webcam Full HD"
    },
    new Produto
    {
        Nome = "SSD 1TB NVMe"
    },
    new Produto
    {
        Nome = "Cadeira Gamer"
    },
    new Produto
    {
        Nome = "Smartphone Max 256GB"
    },
    new Produto
    {
        Nome = "Tablet 10 Polegadas"
    }
};

//FUNCIONALIDADES - EndPoint
//Requisições
// - Método HTTP
// - URL
//Resposta
// - Dado/Informação

app.MapGet("/", () => "API do Ecommerce");

app.MapGet("/api/produto/listar", () =>
{
    return produtos;
});

app.Run();


