using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minishop.Infrastructure.Entities
{
    public class Image : BaseType
    {
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
