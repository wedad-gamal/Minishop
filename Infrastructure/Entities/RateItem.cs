using System.ComponentModel.DataAnnotations;

namespace Minishop.Infrastructure.Entities
{
    public class RateItem : BaseType
    {
        [MaxLength(50)]
        public string UserName { get; set; }

        [Range(0, 5)]
        public int RateValue { get; set; }

        [MaxLength(250)]
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
