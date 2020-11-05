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
    public class TourDiaDiemsController : Controller
    {
        private readonly TourDBContext context;

        public TourDiaDiemsController()
        {
            context = new TourDBContext();
        }

        // GET: TourDiaDiems
        public IActionResult Index(int PageNumber = 1)
        {
            
            var tourDiaDiems = (from l in context.TourDiaDiem
                         select l).OrderBy(x => x.DiaDiemThanhPho);
            ViewBag.TotalPages = Math.Ceiling(tourDiaDiems.Count() / 5.0);
            var listTourDiaDiem = tourDiaDiems.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourDiaDiem);
        }
        [HttpGet]
        public IActionResult Index(string classify,string searchString, int PageNumber = 1)
        {
           
            IEnumerable<TourDiaDiem> listTourDiaDiem;
            var tourDiaDiems = (from l in context.TourDiaDiem
                                select l).OrderBy(x => x.DiaDiemThanhPho);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourDiaDiems.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Địa điểm thành phố") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourDiaDiem =  tourDiaDiems.Where(s => s.DiaDiemThanhPho.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourDiaDiem.Count() / 5.0);
                return View(listTourDiaDiem.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên địa điểm") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourDiaDiem = tourDiaDiems.Where(s => s.DiaDiemTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourDiaDiem.Count() / 5.0);
                return View(listTourDiaDiem.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Mô tả địa điểm") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourDiaDiem = tourDiaDiems.Where(s => s.DiaDiemMoTa.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourDiaDiem.Count() / 5.0);
                return View(listTourDiaDiem.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            return View(tourDiaDiems.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: TourDiaDiems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDiaDiem = await context.TourDiaDiem
                .FirstOrDefaultAsync(m => m.DiaDiemId == id);
            if (tourDiaDiem == null)
            {
                return NotFound();
            }

            return View(tourDiaDiem);
        }

        // GET: TourDiaDiems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TourDiaDiems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiaDiemId,DiaDiemThanhPho,DiaDiemTen,DiaDiemMoTa,NgayTao")] TourDiaDiem tourDiaDiem)
        {
            if (ModelState.IsValid)
            {
                context.Add(tourDiaDiem);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourDiaDiem);
        }

        // GET: TourDiaDiems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDiaDiem = await context.TourDiaDiem.FindAsync(id);
            if (tourDiaDiem == null)
            {
                return NotFound();
            }
            return View(tourDiaDiem);
        }

        // POST: TourDiaDiems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiaDiemId,DiaDiemThanhPho,DiaDiemTen,DiaDiemMoTa,NgayTao")] TourDiaDiem tourDiaDiem)
        {
            if (id != tourDiaDiem.DiaDiemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tourDiaDiem);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourDiaDiemExists(tourDiaDiem.DiaDiemId))
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
            return View(tourDiaDiem);
        }

        // GET: TourDiaDiems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDiaDiem = await context.TourDiaDiem
                .FirstOrDefaultAsync(m => m.DiaDiemId == id);
            if (tourDiaDiem == null)
            {
                return NotFound();
            }

            return View(tourDiaDiem);
        }

        // POST: TourDiaDiems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourDiaDiem = await context.TourDiaDiem.FindAsync(id);
            context.TourDiaDiem.Remove(tourDiaDiem);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourDiaDiemExists(int id)
        {
            return context.TourDiaDiem.Any(e => e.DiaDiemId == id);
        }
    }
}
