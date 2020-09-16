using System.ComponentModel.DataAnnotations;

namespace MinhaAppMvcDesafio.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        [Key]
        public int Id { get; set; }
    }
}