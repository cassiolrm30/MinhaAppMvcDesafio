using System.Threading.Tasks;
using MinhaAppMvcDesafio.Models;

namespace MinhaAppMvcDesafio.Repositories.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(int id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(int id);
        int ObterQtdPaginas();
    }
}