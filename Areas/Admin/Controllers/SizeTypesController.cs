using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minishop.Application.Contracts;
using Minishop.Infrastructure;
using Minishop.Infrastructure.Entities;
using Minishop.Infrastructure.ViewModels;

namespace Minishop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeTypesController : Controller
    {
        private readonly MinishopDBContext _context;
        private readonly ISizeTypeServices _sizeTypeServices;

        public SizeTypesController(MinishopDBContext context, ISizeTypeServices sizeTypeServices)
        {
            _context = context;
            _sizeTypeServices = sizeTypeServices;
        }

        // GET: Admin/SizeTypes
        public async Task<IActionResult> Index()
        {
            return _context.SizeTypes != null ?
                        View(await _context.SizeTypes.ToListAsync()) :
                        Problem("Entity set 'MinishopDBContext.SizeTypes'  is null.");
        }

        // GET: Admin/SizeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SizeTypes == null)
            {
                return NotFound();
            }

            var sizeType = await _context.SizeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sizeType == null)
            {
                return NotFound();
            }

            return View(sizeType);
        }

        // GET: Admin/SizeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SizeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] SizeTypeViewModel sizeType)
        {
            if (ModelState.IsValid)
            {
                _sizeTypeServices.AddSizeType(sizeType.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(sizeType);
        }

        // GET: Admin/SizeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SizeTypes == null)
            {
                return NotFound();
            }

            var sizeType = await _context.SizeTypes.FindAsync(id);
            if (sizeType == null)
            {
                return NotFound();
            }
            return View(sizeType);
        }

        // POST: Admin/SizeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SizeType sizeType)
        {
            if (id != sizeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sizeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeTypeExists(sizeType.Id))
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
            return View(sizeType);
        }

        // GET: Admin/SizeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SizeTypes == null)
            {
                return NotFound();
            }

            var sizeType = await _context.SizeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sizeType == null)
            {
                return NotFound();
            }

            return View(sizeType);
        }

        // POST: Admin/SizeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SizeTypes == null)
            {
                return Problem("Entity set 'MinishopDBContext.SizeTypes'  is null.");
            }
            var sizeType = await _context.SizeTypes.FindAsync(id);
            if (sizeType != null)
            {
                _context.SizeTypes.Remove(sizeType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SizeTypeExists(int id)
        {
            return (_context.SizeTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
