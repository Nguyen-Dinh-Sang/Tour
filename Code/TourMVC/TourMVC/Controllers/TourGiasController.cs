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
    public class TourGiasController : Controller
    {
        private readonly TourDBContext _context;

        public TourGiasController()
        {
            _context = new TourDBContext();
        }

        // GET: TourGias
        public async Task<IActionResult> Index()
        {
            var tourDBContext = _context.TourGia.Include(t => t.Tour);
            return View(await tourDBContext.ToListAsync());
        }

        // GET: TourGias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourGia = await _context.TourGia
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.GiaId == id);
            if (tourGia == null)
            {
                return NotFound();
            }

            return View(tourGia);
        }

        // GET: TourGias/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa");
            return View();
        }

        // POST: TourGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GiaId,GiaSoTien,TourId,GiaTuNgay,GiaDenNgay,NgayTao")] TourGia tourGia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourGia.TourId);
            return View(tourGia);
        }

        // GET: TourGias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourGia = await _context.TourGia.FindAsync(id);
            if (tourGia == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourGia.TourId);
            return View(tourGia);
        }

        // POST: TourGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GiaId,GiaSoTien,TourId,GiaTuNgay,GiaDenNgay,NgayTao")] TourGia tourGia)
        {
            if (id != tourGia.GiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourGiaExists(tourGia.GiaId))
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
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourGia.TourId);
            return View(tourGia);
        }

        // GET: TourGias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourGia = await _context.TourGia
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.GiaId == id);
            if (tourGia == null)
            {
                return NotFound();
            }

            return View(tourGia);
        }

        // POST: TourGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourGia = await _context.TourGia.FindAsync(id);
            _context.TourGia.Remove(tourGia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourGiaExists(int id)
        {
            return _context.TourGia.Any(e => e.GiaId == id);
        }
    }
}
