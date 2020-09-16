using Microsoft.EntityFrameworkCore;
using MinhaAppMvcDesafio.Data;
using MinhaAppMvcDesafio.Models;
using MinhaAppMvcDesafio.Repositories.Interfaces;
using System.Threading.Tasks;

namespace MinhaAppMvcDesafio.Repositories.Classes
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(Contexto context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(int fornecedorId)
        {
            return await contexto.Enderecos.AsNoTracking().FirstOrDefaultAsync(f => f.Id == fornecedorId);
        }
    }
}