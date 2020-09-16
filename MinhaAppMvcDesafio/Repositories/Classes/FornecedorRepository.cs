using Microsoft.EntityFrameworkCore;
using MinhaAppMvcDesafio.Data;
using MinhaAppMvcDesafio.Models;
using MinhaAppMvcDesafio.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAppMvcDesafio.Repositories.Classes
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(Contexto context) : base(context) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(int id)
        {
            return await contexto.Fornecedores.AsNoTracking()
                                 .Include(c => c.Endereco)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(int id)
        {
            return await contexto.Fornecedores.AsNoTracking()
                                 .Include(c => c.Produtos)
                                 .Include(c => c.Endereco)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public int ObterQtdPaginas()
        {
            return (DbSet.Count() / pageSize) + ((DbSet.Count() % pageSize == 0) ? 0 : 1);
        }

        public override async Task<List<Fornecedor>> ObterPaginados(int pageIndex)
        {
            int indice       = pageIndex / 100;
            int posicaoCampo = (pageIndex % 100) / 10;
            int sentidoCampo = pageIndex % 10;
            string campo     = "";

            var registros = DbSet;

            switch (posicaoCampo)
            {
                case 1: campo = "Nome";      break;
                case 2: campo = "Documento"; break;
                case 3: campo = "Ativo";     break;
            }

            var resultado = registros.OrderBy(x => x.GetType().GetProperty(campo).GetValue(x, null));
            if (sentidoCampo == 1)
                resultado = registros.OrderByDescending(x => x.GetType().GetProperty(campo).GetValue(x, null));

            return await resultado.Skip((indice - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}