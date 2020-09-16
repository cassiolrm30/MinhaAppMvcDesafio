using System;
using System.ComponentModel.DataAnnotations;

namespace MinhaAppMvcDesafio.Models
{
    public class ItemPedido : Entity
    {
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Display(Name = "Data de Reposição")]
        public DateTime DataReposicao { get; set; }

        /* EF Relations */
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }
    }
}
