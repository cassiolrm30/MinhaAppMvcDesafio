using System.Threading.Tasks;
using MinhaAppMvcDesafio.Models;

namespace MinhaAppMvcDesafio.Repositories.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(int fornecedorId);
    }
}