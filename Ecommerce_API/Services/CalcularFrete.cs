using Application.DTOs;
using Domain.Entidades;
using Domain.Interfaces;
using Ecommerce_API.Services;


namespace Ecommerce_API.Services;

///*public class CalcularFrete
//{

//    public decimal Calcular(int CepDestino, int CepOrigem = 25
//    {
//        int distancia = Math.Abs(CepDestino - CepOrigem);
//        if (distancia <= 5)
//        {
//            return 10.00m;
//        }
//        else if (distancia > 5 && distancia <= 20)
//        {
//            return 20.00m;
//        }
//        else
//        {
//            return 0.00m;
//        }
//    }
///}
//    //se a distancia for menor que 5km, o frete é R$10,00
//    //se a distancia for entre 5km e 20km, o frete é R$20,00
//    // se a distancia for igual ou menor que 5km, nao a frete