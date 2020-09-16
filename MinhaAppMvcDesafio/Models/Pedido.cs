using System;
using System.ComponentModel.DataAnnotations;

namespace MinhaAppMvcDesafio.Models
{
    public class Pedido : Entity
    {
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        /* EF Relations */
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public IEnumerable<ItemPedido> Itens { get; set; }
    }
}