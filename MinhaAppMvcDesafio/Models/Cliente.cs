using System;
using System.ComponentModel.DataAnnotations;

namespace MinhaAppMvcDesafio.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }
    }
}
