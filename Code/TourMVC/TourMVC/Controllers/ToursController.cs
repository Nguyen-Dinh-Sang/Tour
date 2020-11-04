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
    public class ToursController : Controller
    {
        private readonly TourDBContext context;

        public ToursController()
        {
            context = new TourDBContext();
        }

        // GET: Tours
        public IActionResult Index(int PageNumber = 1)
        {
            var tourDBContext = context.Tour.Include(t => t.Loai);
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            var listTour = tourDBContext.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTour);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, int PageNumber = 1)
        {

            IEnumerable<Tour> listTour;
            var tour = (from l in context.Tour
                             select l).Include(t => t.Loai).OrderBy(x => x.TourTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tour.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên tour") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTour = tour.Where(s => s.TourTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTour.Count() / 5.0);
                return View(listTour.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Mô tả tour") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTour = tour.Where(s => s.TourMoTa.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTour.Count() / 5.0);
                return View(listTour.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Loại tour") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTour = tour.Where(s => s.Loai.LoaiTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTour.Count() / 5.0);
                return View(listTour.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }


            return View(tour.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: Tours/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Tours/Create
        public IActionResult Create()
        {
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiMoTa");
            return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourId,TourTen,TourMoTa,LoaiId,NgayTao")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                context.Add(tour);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiMoTa", tour.LoaiId);
            return View(tour);
        }

        // GET: Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiMoTa", tour.LoaiId);
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TourId,TourTen,TourMoTa,LoaiId,NgayTao")] Tour tour)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiMoTa", tour.LoaiId);
            return View(tour);
        }

        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tour = await context.Tour.FindAsync(id);
            context.Tour.Remove(tour);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return context.Tour.Any(e => e.TourId == id);
        }
    }
}
