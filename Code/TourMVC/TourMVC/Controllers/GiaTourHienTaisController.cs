using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourMVC.Models;

namespace TourMVC.Controllers
{
    public class GiaTourHienTaisController : Controller
    {
        private readonly TourDBContext _context;

        public GiaTourHienTaisController()
        {
            _context = new TourDBContext();
        }

        // GET: GiaTourHienTais
        public async Task<IActionResult> Index()
        {
            var tourDBContext = _context.GiaTourHienTai.Include(g => g.Gia).Include(g => g.Tour);
            return View(await tourDBContext.ToListAsync());
        }

        // GET: GiaTourHienTais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaTourHienTai = await _context.GiaTourHienTai
                .Include(g => g.Gia)
                .Include(g => g.Tour)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (giaTourHienTai == null)
            {
                return NotFound();
            }

            return View(giaTourHienTai);
        }

        // GET: GiaTourHienTais/Create
        public IActionResult Create()
        {
            ViewData["GiaId"] = new SelectList(_context.TourGia, "GiaId", "GiaId");
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa");
            return View();
        }

        // POST: GiaTourHienTais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourId,GiaId,NgayTao")] GiaTourHienTai giaTourHienTai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giaTourHienTai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GiaId"] = new SelectList(_context.TourGia, "GiaId", "GiaId", giaTourHienTai.GiaId);
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", giaTourHienTai.TourId);
            return View(giaTourHienTai);
        }

        // GET: GiaTourHienTais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaTourHienTai = await _context.GiaTourHienTai.FindAsync(id);
            if (giaTourHienTai == null)
            {
                return NotFound();
            }
            ViewData["GiaId"] = new SelectList(_context.TourGia, "GiaId", "GiaId", giaTourHienTai.GiaId);
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", giaTourHienTai.TourId);
            return View(giaTourHienTai);
        }

        // POST: GiaTourHienTais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TourId,GiaId,NgayTao")] GiaTourHienTai giaTourHienTai)
        {
            if (id != giaTourHienTai.TourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giaTourHienTai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiaTourHienTaiExists(giaTourHienTai.TourId))
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
            ViewData["GiaId"] = new SelectList(_context.TourGia, "GiaId", "GiaId", giaTourHienTai.GiaId);
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", giaTourHienTai.TourId);
            return View(giaTourHienTai);
        }

        // GET: GiaTourHienTais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaTourHienTai = await _context.GiaTourHienTai
                .Include(g => g.Gia)
                .Include(g => g.Tour)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (giaTourHienTai == null)
            {
                return NotFound();
            }

            return View(giaTourHienTai);
        }

        // POST: GiaTourHienTais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giaTourHienTai = await _context.GiaTourHienTai.FindAsync(id);
            _context.GiaTourHienTai.Remove(giaTourHienTai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiaTourHienTaiExists(int id)
        {
            return _context.GiaTourHienTai.Any(e => e.TourId == id);
        }
    }
}
