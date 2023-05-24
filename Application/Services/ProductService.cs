using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Minishop.Application.Contracts;
using Minishop.Infrastructure;
using Minishop.Infrastructure.Entities;
using Minishop.Infrastructure.ViewModels;


namespace Minishop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly MinishopDBContext _context;
        private readonly IImageService _imageService;

        public ProductService(MinishopDBContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }
        public int AddProduct(ProductViewModel productViewModel)
        {
            var newProduct = new Product()
            {
                AvailableNumber = productViewModel.AvailableNumber,
                Description = productViewModel.Description,
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                ProductCategoryId = productViewModel.ProductCategoryId,
                ProductTypeId = productViewModel.ProductTypeId,
                SizeTypeId = productViewModel.SizeTypeId,
                SoldNumber = productViewModel.SoldNumber,
                TotalRatedPeople = productViewModel.TotalRatedPeople,
                CalculatedRateValue = productViewModel.CalculatedRateValue
            };
            _context.Add(newProduct);
            _context.SaveChanges();
            if (productViewModel.ImageFormFile != null)
            {
                var imageId = _imageService.SaveImage(productViewModel.ImageFormFile, newProduct.Id);
                newProduct.ImageId = imageId;
                _context.SaveChanges();
            }

            return newProduct.Id;
        }
        public bool DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                _imageService.DeleteImage(productId);
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool EditProduct(int id, ProductViewModel product)
        {
            var productDb = _context.Products.FirstOrDefault(x => x.Id == id);
            if (productDb != null)
            {
                productDb.ProductTypeId = product.ProductTypeId;
                productDb.ProductCategoryId = product.ProductCategoryId;
                productDb.ProductTypeId = product.ProductTypeId;
                productDb.SoldNumber = product.SoldNumber;
                productDb.TotalRatedPeople = product.TotalRatedPeople;
                productDb.CalculatedRateValue = product.CalculatedRateValue;
                productDb.TotalRatedPeople = product.TotalRatedPeople;
                productDb.Name = product.Name;
                productDb.Description = product.Description;
                //productDb.ImageId = product.ImageId;
                productDb.AvailableNumber = product.AvailableNumber;
                productDb.Price = product.Price;
                productDb.SizeTypeId = product.SizeTypeId;

                if (product.ImageFormFile != null)
                {
                    var imageId = _imageService.SaveImage(product.ImageFormFile, id, true);
                    productDb.ImageId = imageId;
                }
                // _context.Update(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public Product GetProductById(int productId)
        {
            var product = _context.Products
              .Include(p => p.ProductCategory)
              .Include(p => p.ProductType)
              .Include(p => p.SizeType)
              .Include(p => p.Image)
              .FirstOrDefault(m => m.Id == productId);

            return product;
        }
        public List<Product> GetProducts()
        {
            var products = _context.Products
                .Include(p => p.Image)
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductType)
                .Include(p => p.SizeType)
                .ToList();

            return products;
        }
    }
}
