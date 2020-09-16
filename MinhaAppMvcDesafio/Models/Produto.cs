using System;
using System.ComponentModel.DataAnnotations;

namespace MinhaAppMvcDesafio.Models
{
    public class Produto : Entity
    {
        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(200)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [StringLength(100)]
        public string Imagem { get; set; }

        [Display(Name = "Valor (R$)")]
        public decimal Valor { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        
        public bool Ativo { get; set; }

        public int Quantidade { get; set; }

        /* EF Relations */
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        //public IEnumerable<Pedido> Pedidos { get; set; }
    }
}