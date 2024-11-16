using AuthUserIdentity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthUserIdentity.Controllers
{
    public class UserroleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserroleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Userrolemasters.ToListAsync());
        }

        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userrolemaster = await _context.Userrolemasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userrolemaster == null)
            {
                return NotFound();
            }

            return View(userrolemaster);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleName,Description,IsActive,IsRemoved")] Userrolemaster userrolemaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userrolemaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userrolemaster);
        }

        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userrolemaster = await _context.Userrolemasters.FindAsync(id);
            if (userrolemaster == null)
            {
                return NotFound();
            }
            return View(userrolemaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,RoleName,Description,IsActive,IsRemoved")] Userrolemaster userrolemaster)
        {
            if (id != userrolemaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userrolemaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserrolemasterExists(userrolemaster.Id))
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
            return View(userrolemaster);
        }

        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userrolemaster = await _context.Userrolemasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userrolemaster == null)
            {
                return NotFound();
            }

            return View(userrolemaster);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var userrolemaster = await _context.Userrolemasters.FindAsync(id);
            if (userrolemaster != null)
            {
                _context.Userrolemasters.Remove(userrolemaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserrolemasterExists(short id)
        {
            return _context.Userrolemasters.Any(e => e.Id == id);
        }
    }
}