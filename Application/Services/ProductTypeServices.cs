using Minishop.Application.Contracts;
using Minishop.Infrastructure;
using Minishop.Infrastructure.Entities;

namespace Minishop.Application.Services
{
    public class ProductTypeServices : IProductTypeServices
    {
        private readonly MinishopDBContext _context;

        public ProductTypeServices(MinishopDBContext context)
        {
            _context = context;
        }
        public void AddProductType(string name)
        {
            _context.Add(new ProductType() { Name = name });
            _context.SaveChanges();
        }
    }
}
