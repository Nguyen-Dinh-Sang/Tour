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
    public class TourLoaiChiPhisController : Controller
    {
        private readonly TourDBContext _context;

        public TourLoaiChiPhisController()
        {
            _context = new TourDBContext();
        }

        // GET: TourLoaiChiPhis
        public async Task<IActionResult> Index()
        {
            return View(await _context.TourLoaiChiPhi.ToListAsync());
        }

        // GET: TourLoaiChiPhis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoaiChiPhi = await _context.TourLoaiChiPhi
                .FirstOrDefaultAsync(m => m.LoaiChiPhiId == id);
            if (tourLoaiChiPhi == null)
            {
                return NotFound();
            }

            return View(tourLoaiChiPhi);
        }

        // GET: TourLoaiChiPhis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TourLoaiChiPhis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiChiPhiId,LoaiChiPhiTen,LoaiChiPhiMoTa,NgayTao")] TourLoaiChiPhi tourLoaiChiPhi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourLoaiChiPhi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourLoaiChiPhi);
        }

        // GET: TourLoaiChiPhis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoaiChiPhi = await _context.TourLoaiChiPhi.FindAsync(id);
            if (tourLoaiChiPhi == null)
            {
                return NotFound();
            }
            return View(tourLoaiChiPhi);
        }

        // POST: TourLoaiChiPhis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiChiPhiId,LoaiChiPhiTen,LoaiChiPhiMoTa,NgayTao")] TourLoaiChiPhi tourLoaiChiPhi)
        {
            if (id != tourLoaiChiPhi.LoaiChiPhiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourLoaiChiPhi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourLoaiChiPhiExists(tourLoaiChiPhi.LoaiChiPhiId))
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
            return View(tourLoaiChiPhi);
        }

        // GET: TourLoaiChiPhis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoaiChiPhi = await _context.TourLoaiChiPhi
                .FirstOrDefaultAsync(m => m.LoaiChiPhiId == id);
            if (tourLoaiChiPhi == null)
            {
                return NotFound();
            }

            return View(tourLoaiChiPhi);
        }

        // POST: TourLoaiChiPhis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourLoaiChiPhi = await _context.TourLoaiChiPhi.FindAsync(id);
            _context.TourLoaiChiPhi.Remove(tourLoaiChiPhi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourLoaiChiPhiExists(int id)
        {
            return _context.TourLoaiChiPhi.Any(e => e.LoaiChiPhiId == id);
        }
    }
}
