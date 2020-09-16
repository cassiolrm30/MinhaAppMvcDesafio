using Microsoft.EntityFrameworkCore;
using MinhaAppMvcDesafio.Data;
using MinhaAppMvcDesafio.Models;
using MinhaAppMvcDesafio.Repositories.Interfaces;
using MinhaAppMvcDesafio.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAppMvcDesafio.Repositories.Classes
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(Contexto context) : base(context) { pageSize = 5; }

        public async Task<Produto> ObterProdutoFornecedor(int id)
        {
            return await contexto.Produtos.AsNoTracking().Include(f => f.Fornecedor)
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await contexto.Produtos.AsNoTracking().Include(f => f.Fornecedor)
                                          .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(int fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }

        public int ObterQtdPaginas()
        {
            return (DbSet.Count() / pageSize) + ((DbSet.Count() % pageSize == 0) ? 0 : 1);
        }

        public override async Task<List<Produto>> ObterPaginados(int pageIndex)
        {
            int indice       = pageIndex / 100;
            int posicaoCampo = (pageIndex % 100) / 10;
            int sentidoCampo = pageIndex % 10;
            string campo     = "";

            var registros = DbSet.Include(x => x.Fornecedor);

            switch (posicaoCampo)
            {
                case 1: campo = "Nome";         break;
                case 2: campo = "FornecedorId"; break;
                case 3: campo = "Valor";        break;
                case 4: campo = "Ativo";        break;
            }

            var resultado = registros.OrderBy(x => x.GetType().GetProperty(campo).GetValue(x, null));
            if (sentidoCampo == 1)
                resultado = registros.OrderByDescending(x => x.GetType().GetProperty(campo).GetValue(x, null));

            return await resultado.Skip((indice - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        //public async Task<List<Produto>> MovimentacaoEstoque(int produtoId, int pageIndex)
        //{
        //    int indice       = pageIndex / 100;
        //    int posicaoCampo = (pageIndex % 100) / 10;
        //    int sentidoCampo = pageIndex % 10;
        //    string campo     = "";

        //    var produtos     = DbSet;
        //    var produtoSelecionado = DbSet.FirstOrDefault(x => x.Id == produtoId);
        //    var itensPedido  = new List<ItemPedido>();
        //    var itensPedidos =  from pro in contexto.ItensPedidos
        //                       where pro.ProdutoId == produtoId
        //                      select new ItemPedido() { DataReposicao = pro.DataReposicao, Quantidade = pro.Quantidade };

        //    var registros    = new List<EstoqueViewModel>();
        //    int estoque      = produtoSelecionado.Quantidade;

        //    foreach (Produto produto in produtos)
        //    {
        //        var item = new EstoqueViewModel();
        //        item.Data = "11/11/1111";
        //        item.Hora = "11:11";
        //        item.Transacao = "Ajuste de Estoque";
        //        item.Quantidade = 1;
        //        item.ValorTotal = decimal.Parse((produto.Valor * item.Quantidade).ToString());
        //        item.Estoque = estoque + item.Quantidade;
        //        registros.Add(item);
        //    }

        //    foreach (ItemPedido itemPedido in itensPedido)
        //    {
        //        var item = new EstoqueViewModel();
        //        item.Data = "11/11/1111";
        //        item.Hora = "11:11";
        //        item.Transacao = "Venda";
        //        item.Quantidade = itemPedido.Produto.Quantidade;
        //        item.ValorTotal = decimal.Parse((itemPedido.Produto.Valor * item.Quantidade).ToString());
        //        item.Estoque = estoque - item.Quantidade;
        //        registros.Add(item);
        //    }

        //    switch (posicaoCampo)
        //    {
        //        case 1: campo = "Data";       break;
        //        case 2: campo = "Hora";       break;
        //        case 3: campo = "Transacao";  break;
        //        case 4: campo = "ValorTotal"; break;
        //        case 5: campo = "Quantidade"; break;
        //        case 6: campo = "Estoque";    break;
        //    }

        //    var resultado = registros.OrderBy(x => x.GetType().GetProperty(campo).GetValue(x, null));
        //    if (sentidoCampo == 1)
        //        resultado = registros.OrderByDescending(x => x.GetType().GetProperty(campo).GetValue(x, null));

        //    return await resultado.Skip((indice - 1) * pageSize).Take(pageSize).ToListAsync();
        //}
    }
}