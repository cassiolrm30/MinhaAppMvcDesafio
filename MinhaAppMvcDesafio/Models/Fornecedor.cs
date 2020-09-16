using System.ComponentModel.DataAnnotations;

namespace MinhaAppMvcDesafio.Models
{
    public class Fornecedor : Entity
    {
        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(20)]
        public string Documento { get; set; }

        public TipoFornecedor TipoFornecedor { get; set; }

        public bool Ativo { get; set; }

        /* EF Relations */
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
    }
}