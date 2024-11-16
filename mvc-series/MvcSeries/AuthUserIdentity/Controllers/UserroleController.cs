using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthUserIdentity.Models;

namespace AuthUserIdentity.Controllers
{
    public class UserroleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserroleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Userrole
        public async Task<IActionResult> Index()
        {
            return View(await _context.Userrolemasters.ToListAsync());
        }

        // GET: Userrole/Details/5
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

        // GET: Userrole/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Userrole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Userrole/Edit/5
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

        // POST: Userrole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Userrole/Delete/5
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

        // POST: Userrole/Delete/5
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
