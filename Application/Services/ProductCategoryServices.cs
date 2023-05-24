using Minishop.Application.Contracts;
using Minishop.Infrastructure;
using Minishop.Infrastructure.Entities;

namespace Minishop.Application.Services
{
    public class ProductCategoryServices : IProductCategoryServices
    {
        private readonly MinishopDBContext _context;

        public ProductCategoryServices(MinishopDBContext context)
        {
            _context = context;
        }
        public void AddProductCategory(string name)
        {
            _context.Add(new ProductCategory() { Name = name });
            _context.SaveChanges();
        }
    }
}
