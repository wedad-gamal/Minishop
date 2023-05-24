using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minishop.Application.Contracts;
using Minishop.Infrastructure;
using Minishop.Infrastructure.Entities;
using Minishop.Infrastructure.ViewModels;

namespace Minishop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly MinishopDBContext _context;
        private readonly IProductTypeServices _productTypeServices;

        public ProductTypesController(MinishopDBContext context, IProductTypeServices productTypeServices)
        {
            _context = context;
            _productTypeServices = productTypeServices;
        }

        // GET: Admin/ProductTypes
        public async Task<IActionResult> Index()
        {
            return _context.ProductTypes != null ?
                        View(await _context.ProductTypes.ToListAsync()) :
                        Problem("Entity set 'MinishopDBContext.ProductTypes'  is null.");
        }

        // GET: Admin/ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: Admin/ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] ProductTypeViewModel productType)
        {
            if (ModelState.IsValid)
            {
                _productTypeServices.AddProductType(productType.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: Admin/ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: Admin/ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: Admin/ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: Admin/ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductTypes == null)
            {
                return Problem("Entity set 'MinishopDBContext.ProductTypes'  is null.");
            }
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(int id)
        {
            return (_context.ProductTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
