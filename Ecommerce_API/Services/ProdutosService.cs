using Application.DTOs;
using Domain.Entidades;
using Domain.Interfaces;
using Domain.Helpers;

namespace Ecommerce_API.Services;

public class ProdutosService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProdutoRepositoryJson _produtoRepositoryJson;

    public ProdutosService(IProdutoRepository produtoRepository, IProdutoRepositoryJson produtoRepositoryJson)
    {
        _produtoRepository = produtoRepository;
        _produtoRepositoryJson = produtoRepositoryJson;
    }

    public void Incluir(Produto produto)
    {
        if (IsProdutoInvalido(produto))
            throw new ArgumentException("Todos os dados do produto devem ser devidamente preenchidos!!!");
        _produtoRepository.Incluir(produto);
        _produtoRepositoryJson.SalvarNoArquivo();
    }

    public List<Produto> Listar()
    {
        try
        {
            List<Produto> listaProdutos = _produtoRepository.Listar()
                ?? throw new DomainException("Nenhum produto foi encontrado.");
            if (listaProdutos.Count == 0)
                throw new DomainException("Nenhum produto disponível para listar.");
            //List<ProdutoDTO> listaProdutosDTO = new List<ProdutoDTO>();
            //foreach (var produto in listaProdutos)
            //{
            //    if (produto == null)
            //        throw new DomainException("Produto inválido encontrado na coleção.");
            //    // Mapear Produto para ProdutoDTO
            //    listaProdutosDTO.Add(new ProdutoDTO
            //    {
            //        Id = produto.Id,
            //        Nome = produto.Nome,
            //    });
            //}
            //return listaProdutosDTO;

            return listaProdutos;
        }
        catch (DomainException)
        {
            // Repassa a exceção de domínio para o controller tratar
            throw;
        }
        catch (Exception ex)
        {
            // Qualquer erro inesperado é encapsulado
            throw new Exception("Erro ao listar produtos.", ex);
        }
    }
    public void Remover(int id)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("Id do produto inválido.");
            bool removido = _produtoRepository.Remover(id);
            if (!removido)
                throw new DomainException("Produto não encontrado para remoção.");
            else
                _produtoRepositoryJson.SalvarNoArquivo();
        }
        catch (DomainException)
        {
            throw; 
        }
        catch (ArgumentException)
        {
            throw; 
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao remover o produto.", ex);
        }
    }


    public void AtualizarQuantidade(int produtoId, int quantidade)
    {
        try
        {
            if (produtoId <= 0)
                throw new ArgumentException("Id do produto inválido.");
            if (quantidade < 0)
                throw new ArgumentException("Quantidade não pode ser negativa.");
            Produto produto = _produtoRepository.Listar()
                .FirstOrDefault(p => p.Id == produtoId)
                ?? throw new DomainException("Produto não encontrado.");
            // regra de negócio: atualizar o estoque
            produto.Quantidade = quantidade;
            _produtoRepository.AtualizarQuantidade(produtoId, quantidade);
            _produtoRepositoryJson.SalvarNoArquivo();
        }
        catch (DomainException)
        {
            throw; 
        }
        catch (ArgumentException)
        {
            throw; 
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao atualizar o estoque do produto.", ex);
        }
    }

    private bool IsProdutoInvalido(Produto produto)
    {
        if (string.IsNullOrWhiteSpace(produto.Nome) || produto.Preco == 0 || produto.Id == 0 || produto.Quantidade == 0)
            return true;
        return false;
    }
}
