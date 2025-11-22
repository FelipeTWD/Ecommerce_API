using Application.DTOs;
using Domain.Entidades;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Helpers;


namespace Ecommerce_API.Services
{
    public class CarrinhoService
    {
        private ICarrinhoRepository _carrinhoRepository;
        public CarrinhoService(ICarrinhoRepository carrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
        }
        public List<CarrinhoDTO> Listar()
        {
            try
            {
                var listaCarrinhos = _carrinhoRepository.Listar()
                    ?? throw new DomainException("Nenhum carrinho foi encontrado.");

                if (listaCarrinhos.Count == 0)
                    throw new DomainException("Nenhum carrinho disponível para listar.");

                List<CarrinhoDTO> listaCarrinhosDTO = new List<CarrinhoDTO>();

                foreach (Carrinho carrinho in listaCarrinhos)
                {
                    if (carrinho == null)
                        // Validação adicional para carrinho nulo
                        throw new DomainException("Carrinho inválido encontrado na coleção.");

                    var dto = new CarrinhoDTO
                    {
                        IdCarrinho = carrinho.IdCarrinho,
                        ClienteId = carrinho.ClienteId,
                        ListaItensCarrinho = carrinho.ListaItensCarrinho?.Select(item =>
                        {
                            if (item == null)
                                throw new DomainException("Item de carrinho inválido.");

                            if (item.Quantidade <= 0)
                                throw new DomainException($"Item com quantidade inválida (IdProduto: {item.IdProduto}).");

                            return new ItemCarrinhoDTO
                            {
                                IdProduto = item.IdProduto,
                                Quantidade = item.Quantidade
                            };
                        }).ToList() ?? new List<ItemCarrinhoDTO>()
                    };

                    listaCarrinhosDTO.Add(dto);
                }

                return listaCarrinhosDTO;
            }
            catch (DomainException)
            {
                // Repassa a exceção de domínio para o controller tratar
                throw;
            }
            catch (Exception ex)
            {
                // Qualquer erro inesperado é encapsulado
                throw new Exception("Erro ao listar carrinhos.", ex);
            }
        }
        public void Remover(int id)
        {
            _carrinhoRepository.Remover(id);
        }
        public decimal CalcularTotal()
        {
            return _carrinhoRepository.CalcularTotal();
        }
    }
}
