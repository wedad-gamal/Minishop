using System.ComponentModel.DataAnnotations;

namespace Minishop.Infrastructure.Entities
{
    public class BaseType
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
