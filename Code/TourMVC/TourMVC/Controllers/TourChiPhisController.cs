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
    public class TourChiPhisController : Controller
    {
        private readonly TourDBContext _context;

        public TourChiPhisController()
        {
            _context = new TourDBContext();
        }

        // GET: TourChiPhis
        public async Task<IActionResult> Index()
        {
            var tourDBContext = _context.TourChiPhi.Include(t => t.Doan);
            return View(await tourDBContext.ToListAsync());
        }

        // GET: TourChiPhis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhi = await _context.TourChiPhi
                .Include(t => t.Doan)
                .FirstOrDefaultAsync(m => m.ChiPhiId == id);
            if (tourChiPhi == null)
            {
                return NotFound();
            }

            return View(tourChiPhi);
        }

        // GET: TourChiPhis/Create
        public IActionResult Create()
        {
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet");
            return View();
        }

        // POST: TourChiPhis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiPhiId,DoanId,ChiPhiTong,NgayTao")] TourChiPhi tourChiPhi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourChiPhi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", tourChiPhi.DoanId);
            return View(tourChiPhi);
        }

        // GET: TourChiPhis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhi = await _context.TourChiPhi.FindAsync(id);
            if (tourChiPhi == null)
            {
                return NotFound();
            }
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", tourChiPhi.DoanId);
            return View(tourChiPhi);
        }

        // POST: TourChiPhis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiPhiId,DoanId,ChiPhiTong,NgayTao")] TourChiPhi tourChiPhi)
        {
            if (id != tourChiPhi.ChiPhiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourChiPhi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourChiPhiExists(tourChiPhi.ChiPhiId))
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
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", tourChiPhi.DoanId);
            return View(tourChiPhi);
        }

        // GET: TourChiPhis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhi = await _context.TourChiPhi
                .Include(t => t.Doan)
                .FirstOrDefaultAsync(m => m.ChiPhiId == id);
            if (tourChiPhi == null)
            {
                return NotFound();
            }

            return View(tourChiPhi);
        }

        // POST: TourChiPhis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourChiPhi = await _context.TourChiPhi.FindAsync(id);
            _context.TourChiPhi.Remove(tourChiPhi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourChiPhiExists(int id)
        {
            return _context.TourChiPhi.Any(e => e.ChiPhiId == id);
        }
    }
}
