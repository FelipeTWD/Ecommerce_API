using Domain.Interfaces;
using Ecommerce_API.Services;
using Infrastructure.Repositorios;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string caminhoBase = builder.Environment.ContentRootPath;
string caminhoJson = Path.Combine(caminhoBase, "App_Data", "produtos.json");

// Garante que a pasta existe
string? pastaJson = Path.GetDirectoryName(caminhoJson);
if (!string.IsNullOrEmpty(pastaJson))
{
    Directory.CreateDirectory(pastaJson);
}

// adicionar serviços ao contêiner.
builder.Services.AddScoped<ProdutosService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoRepositoryJson, ProdutoRepositoryJson>(provider => new ProdutoRepositoryJson(caminhoJson));

// ?? Serviço do carrinho (sem repositório)
builder.Services.AddScoped<CarrinhoService>();
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

// configurar o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

