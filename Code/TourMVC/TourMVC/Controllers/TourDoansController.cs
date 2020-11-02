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
    public class TourDoansController : Controller
    {
        private readonly TourDBContext _context;

        public TourDoansController()
        {
            _context = new TourDBContext();
        }

        // GET: TourDoans
        public async Task<IActionResult> Index()
        {
            var tourDBContext = _context.TourDoan.Include(t => t.Tour);
            return View(await tourDBContext.ToListAsync());
        }

        // GET: TourDoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDoan = await _context.TourDoan
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.DoanId == id);
            if (tourDoan == null)
            {
                return NotFound();
            }

            return View(tourDoan);
        }

        // GET: TourDoans/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa");
            return View();
        }

        // POST: TourDoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoanId,TourId,DoanTen,DoanNgayDi,DoanNgayVe,DoanChiTiet,DoanGiaTour,NgayTao")] TourDoan tourDoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourDoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourDoan.TourId);
            return View(tourDoan);
        }

        // GET: TourDoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDoan = await _context.TourDoan.FindAsync(id);
            if (tourDoan == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourDoan.TourId);
            return View(tourDoan);
        }

        // POST: TourDoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoanId,TourId,DoanTen,DoanNgayDi,DoanNgayVe,DoanChiTiet,DoanGiaTour,NgayTao")] TourDoan tourDoan)
        {
            if (id != tourDoan.DoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourDoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourDoanExists(tourDoan.DoanId))
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
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourDoan.TourId);
            return View(tourDoan);
        }

        // GET: TourDoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDoan = await _context.TourDoan
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.DoanId == id);
            if (tourDoan == null)
            {
                return NotFound();
            }

            return View(tourDoan);
        }

        // POST: TourDoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourDoan = await _context.TourDoan.FindAsync(id);
            _context.TourDoan.Remove(tourDoan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourDoanExists(int id)
        {
            return _context.TourDoan.Any(e => e.DoanId == id);
        }
    }
}
