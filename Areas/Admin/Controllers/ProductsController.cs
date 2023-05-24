using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Minishop.Application.Contracts;
using Minishop.Infrastructure;
using Minishop.Infrastructure.ViewModels;

namespace Minishop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly MinishopDBContext _context;
        private readonly IProductService _productService;

        public ProductsController(MinishopDBContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        // GET: Admin/Products
        public IActionResult Index()
        {
            var products = _productService.GetProducts();

            /* var products = _context.Products
                 .Include(p => p.Image)
                 .Include(p => p.ProductCategory)
                 .Include(p => p.ProductType)
                 .Include(p => p.SizeType)
                 .ToList();*/
            return View(products);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name");
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name");
            ViewData["SizeTypeId"] = new SelectList(_context.SizeTypes, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,ImageFormFIle,ProductCategoryId,ProductTypeId,Price,CalculatedRateValue,TotalRatedPeople,SoldNumber,SizeTypeId,AvailableNumber,Name")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", product.ProductCategoryId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductTypeId);
            ViewData["SizeTypeId"] = new SelectList(_context.SizeTypes, "Id", "Name", product.SizeTypeId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ProductViewModel productViewModel = product;
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", product.ProductCategoryId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductTypeId);
            ViewData["SizeTypeId"] = new SelectList(_context.SizeTypes, "Id", "Name", product.SizeTypeId);
            return View(productViewModel);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,ImageFormFIle,ProductCategoryId,ProductTypeId,Price,CalculatedRateValue,TotalRatedPeople,SoldNumber,SizeTypeId,AvailableNumber,Name")] ProductViewModel product)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _productService.EditProduct(id, product);
                }
                catch (Exception e)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", product.ProductCategoryId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductTypeId);
            ViewData["SizeTypeId"] = new SelectList(_context.SizeTypes, "Id", "Name", product.SizeTypeId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_productService.DeleteProduct(id))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(id);
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
