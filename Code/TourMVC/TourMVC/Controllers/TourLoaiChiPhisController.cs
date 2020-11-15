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
        private readonly TourDBContext context;

        public TourLoaiChiPhisController()
        {
            context = new TourDBContext();
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.TourLoaiChiPhi.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoaiChiPhi = await context.TourLoaiChiPhi
                .FirstOrDefaultAsync(m => m.LoaiChiPhiId == id);
            if (tourLoaiChiPhi == null)
            {
                return NotFound();
            }

            return View(tourLoaiChiPhi);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiChiPhiId,LoaiChiPhiTen,LoaiChiPhiMoTa,NgayTao")] TourLoaiChiPhi tourLoaiChiPhi)
        {
            if (ModelState.IsValid)
            {
                context.Add(tourLoaiChiPhi);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourLoaiChiPhi);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoaiChiPhi = await context.TourLoaiChiPhi.FindAsync(id);
            if (tourLoaiChiPhi == null)
            {
                return NotFound();
            }
            return View(tourLoaiChiPhi);
        }

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
                    context.Update(tourLoaiChiPhi);
                    await context.SaveChangesAsync();
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoaiChiPhi = await context.TourLoaiChiPhi
                .FirstOrDefaultAsync(m => m.LoaiChiPhiId == id);
            if (tourLoaiChiPhi == null)
            {
                return NotFound();
            }

            return View(tourLoaiChiPhi);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourLoaiChiPhi = await context.TourLoaiChiPhi.FindAsync(id);
            context.TourLoaiChiPhi.Remove(tourLoaiChiPhi);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourLoaiChiPhiExists(int id)
        {
            return context.TourLoaiChiPhi.Any(e => e.LoaiChiPhiId == id);
        }
    }
}
