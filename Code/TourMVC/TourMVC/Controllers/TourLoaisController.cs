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
    public class TourLoaisController : Controller
    {
        private readonly TourDBContext context;
        private static int? loaiTourId;

        public TourLoaisController()
        {
            context = new TourDBContext();
        }

        // GET: TourLoais
        public async Task<IActionResult> Index()
        {
            return View(await context.TourLoai.ToListAsync());
        }

        // GET: TourLoais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoai = await context.TourLoai
                .FirstOrDefaultAsync(m => m.LoaiId == id);
            context.Entry(tourLoai).Collection(tl => tl.Tour).Load();
            if (tourLoai == null)
            {
                return NotFound();
            }
            loaiTourId = id;
            return View(tourLoai);
        }

        // GET: TourLoais/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiId,LoaiTen,LoaiMoTa,NgayTao")] TourLoai tourLoai)
        {
            if (ModelState.IsValid)
            {
                context.Add(tourLoai);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourLoai);
        }

        // GET: TourLoais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoai = await context.TourLoai.FindAsync(id);
            if (tourLoai == null)
            {
                return NotFound();
            }
            return View(tourLoai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiId,LoaiTen,LoaiMoTa,NgayTao")] TourLoai tourLoai)
        {
            if (id != tourLoai.LoaiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tourLoai);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourLoaiExists(tourLoai.LoaiId))
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
            return View(tourLoai);
        }

        // GET: TourLoais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoai = await context.TourLoai
                .FirstOrDefaultAsync(m => m.LoaiId == id);
            if (tourLoai == null)
            {
                return NotFound();
            }

            return View(tourLoai);
        }

        // POST: TourLoais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourLoai = await context.TourLoai.FindAsync(id);
            context.TourLoai.Remove(tourLoai);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourLoaiExists(int id)
        {
            return context.TourLoai.Any(e => e.LoaiId == id);
        }

        // GET: TourLoais/EditTour/5
        public async Task<IActionResult> EditTour(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await context.Tour.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiTen", tour.LoaiId);
            return View(tour);
        }

        // POST: TourLoais/EditTour/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTour(int id, [Bind("TourId,TourTen,TourMoTa,LoaiId,NgayTao")] Tour tour)
        {
            if (id != tour.TourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tour);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.TourId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = loaiTourId });
            }
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiTen", tour.LoaiId);
            return View(tour);
        }

        private bool TourExists(int id)
        {
            return context.Tour.Any(e => e.TourId == id);
        }

        // GET: TourLoais/DeleteTour/5
        public async Task<IActionResult> DeleteTour(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await context.Tour
                .Include(t => t.Loai)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: TourLoais/DeleteTour/5
        [HttpPost, ActionName("DeleteTour")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTourConfirmed(int id)
        {
            var tour = await context.Tour.FindAsync(id);
            context.Tour.Remove(tour);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Details),new { id = tour.LoaiId });
        }
    }
}
