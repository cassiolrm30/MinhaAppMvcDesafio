using AutoMapper;
using MinhaAppMvcDesafio.ViewModels;
using MinhaAppMvcDesafio.Models;

namespace MinhaAppMvcDesafio.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Produto, EstoqueViewModel>().ReverseMap();
        }
    }
}