using System.ComponentModel.DataAnnotations;

namespace MinhaAppMvcDesafio.Models
{
    public class Endereco : Entity
    {
        [StringLength(200)]
        public string Logradouro { get; set; }
        
        [StringLength(5)]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [StringLength(20)]
        public string Complemento { get; set; }

        [StringLength(8)]
        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [StringLength(20)]
        public string Bairro { get; set; }

        [StringLength(50)]
        public string Cidade { get; set; }

        [StringLength(2)]
        [Display(Name = "Estado (UF)")]
        public string Estado { get; set; }

        /* EF Relations */
        public Fornecedor Fornecedor { get; set; }
    }
}