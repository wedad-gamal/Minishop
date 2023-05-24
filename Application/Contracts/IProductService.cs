using Minishop.Infrastructure.Entities;
using Minishop.Infrastructure.ViewModels;

namespace Minishop.Application.Contracts
{
    public interface IProductService
    {
        int AddProduct(ProductViewModel productViewModel);
        List<Product> GetProducts();
        Product GetProductById(int productId);
        bool EditProduct(int id, ProductViewModel product);
        bool DeleteProduct(int productId);

    }
}
