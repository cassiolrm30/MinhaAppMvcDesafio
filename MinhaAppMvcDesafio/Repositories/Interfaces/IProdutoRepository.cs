using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaAppMvcDesafio.Models;
using MinhaAppMvcDesafio.ViewModels;

namespace MinhaAppMvcDesafio.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(int fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        Task<Produto> ObterProdutoFornecedor(int id);
        int ObterQtdPaginas();
    }
}