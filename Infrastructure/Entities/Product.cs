using System.ComponentModel.DataAnnotations;

namespace Minishop.Infrastructure.Entities
{
    public class Product : BaseType
    {
        [MaxLength(250)]
        public string Description { get; set; }

        public int? ImageId { get; set; }
        public Image Image { get; set; }

        public int? ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public int? ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public double Price { get; set; }

        [Range(0, 5)]
        public int CalculatedRateValue { get; set; }
        public int TotalRatedPeople { get; set; }
        public int SoldNumber { get; set; }
        public int? SizeTypeId { get; set; }
        public SizeType SizeType { get; set; }
        public int AvailableNumber { get; set; }

        public List<RateItem> RateItems { get; set; }

    }
}
