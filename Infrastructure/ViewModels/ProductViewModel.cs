using Minishop.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Minishop.Infrastructure.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; } = 0;

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
        public IFormFile? ImageFormFile { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? ProductTypeId { get; set; }
        public double Price { get; set; }

        [Range(0, 5)]
        public int CalculatedRateValue { get; set; }
        public int TotalRatedPeople { get; set; }
        public int SoldNumber { get; set; }
        public int? SizeTypeId { get; set; }
        public int AvailableNumber { get; set; }

        public static implicit operator ProductViewModel(Product v)
        {
            return new ProductViewModel()
            {
                Id = v.Id,
                Name = v.Name,
                Description = v.Description,
                AvailableNumber = v.AvailableNumber,
                ProductCategoryId = v.ProductCategoryId,
                ProductTypeId = v.ProductTypeId,
                CalculatedRateValue = v.CalculatedRateValue,
                SizeTypeId = v.SizeTypeId,
                Price = v.Price,
                SoldNumber = v.SoldNumber,
                TotalRatedPeople = v.TotalRatedPeople

            };
        }
    }
}
