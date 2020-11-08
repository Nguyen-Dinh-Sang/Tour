using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourMVC.Models;
//Quản lý tour - địa điểm vd: xem các tour đi địa điểm
namespace TourMVC.Controllers
{
    public class TourChiTietsController : Controller
    {
        private readonly TourDBContext context;

        public TourChiTietsController()
        {
            context = new TourDBContext();
        }

        // GET: TourChiTiets
        public IActionResult Index(int PageNumber = 1)
        {
            var tourDBContext = context.TourChiTiet.Include(t => t.DiaDiem).Include(t => t.Tour);
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            var listTourDBContext = tourDBContext.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourDBContext);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, int PageNumber = 1)
        {

            IEnumerable<TourChiTiet> listTourChiTiet;
            var tourDBContext = context.TourChiTiet.Include(t => t.DiaDiem).Include(t => t.Tour).OrderBy(x => x.DiaDiem);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên địa điểm") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourChiTiet = tourDBContext.Where(s => s.DiaDiem.DiaDiemTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourChiTiet.Count() / 5.0);
                return View(listTourChiTiet.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            return View(tourDBContext.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: TourChiTiets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiTiet = await context.TourChiTiet
                .Include(t => t.DiaDiem)
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.ChiTietId == id);
            if (tourChiTiet == null)
            {
                return NotFound();
            }

            return View(tourChiTiet);
        }

        // GET: TourChiTiets/Create
        public IActionResult Create()
        {
            ViewData["DiaDiemId"] = new SelectList(context.TourDiaDiem, "DiaDiemId", "DiaDiemTen");
            ViewData["TourId"] = new SelectList(context.Tour, "TourId", "TourTen");
            return View();
        }

        // POST: TourChiTiets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiTietId,TourId,DiaDiemId,ChiTietThuTu,NgayTao")] TourChiTiet tourChiTiet)
        {
            if (ModelState.IsValid)
            {
                context.Add(tourChiTiet);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiaDiemId"] = new SelectList(context.TourDiaDiem, "DiaDiemId", "DiaDiemTen", tourChiTiet.DiaDiemId);
            ViewData["TourId"] = new SelectList(context.Tour, "TourId", "TourTen", tourChiTiet.TourId);
            return View(tourChiTiet);
        }

        // GET: TourChiTiets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiTiet = await context.TourChiTiet.FindAsync(id);
            if (tourChiTiet == null)
            {
                return NotFound();
            }
            ViewData["DiaDiemId"] = new SelectList(context.TourDiaDiem, "DiaDiemId", "DiaDiemTen", tourChiTiet.DiaDiemId);
            ViewData["TourId"] = new SelectList(context.Tour, "TourId", "TourTen", tourChiTiet.TourId);
            return View(tourChiTiet);
        }

        // POST: TourChiTiets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiTietId,TourId,DiaDiemId,ChiTietThuTu,NgayTao")] TourChiTiet tourChiTiet)
        {
            if (id != tourChiTiet.ChiTietId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tourChiTiet);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourChiTietExists(tourChiTiet.ChiTietId))
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
            ViewData["DiaDiemId"] = new SelectList(context.TourDiaDiem, "DiaDiemId", "DiaDiemTen", tourChiTiet.DiaDiemId);
            ViewData["TourId"] = new SelectList(context.Tour, "TourId", "TourTen", tourChiTiet.TourId);
            return View(tourChiTiet);
        }

        // GET: TourChiTiets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiTiet = await context.TourChiTiet
                .Include(t => t.DiaDiem)
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.ChiTietId == id);
            if (tourChiTiet == null)
            {
                return NotFound();
            }

            return View(tourChiTiet);
        }

        // POST: TourChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourChiTiet = await context.TourChiTiet.FindAsync(id);
            context.TourChiTiet.Remove(tourChiTiet);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourChiTietExists(int id)
        {
            return context.TourChiTiet.Any(e => e.ChiTietId == id);
        }
    }
}
