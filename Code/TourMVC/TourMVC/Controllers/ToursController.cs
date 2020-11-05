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
        private static int? tourId;

        public ToursController()
        {
            context = new TourDBContext();
        }

        // GET: Tours
        public IActionResult Index(int PageNumber = 1)
        {
            var tours = context.Tour.Include(t => t.Loai).Include(t2 => t2.GiaTourHienTai.Gia);
            ViewBag.TotalPages = Math.Ceiling(tours.Count() / 5.0);
            var listTour = tours.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTour);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, int PageNumber = 1)
        {

            IEnumerable<Tour> listTour;
            var tour = (from l in context.Tour
                             select l).Include(t => t.Loai).Include(t2 => t2.GiaTourHienTai.Gia).OrderBy(x => x.TourTen);
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
            context.Entry(tour).Collection(t => t.TourChiTiet).Query().Include(tct => tct.DiaDiem).OrderBy(tct => tct.ChiTietThuTu).Load();
           // context.Entry(tour).Reference(t => t.TourChiTiet.Select(tct => tct.DiaDiem)).Load();
            if (tour == null)
            {
                return NotFound();
            }
            tourId = id;
            return View(tour);
        }

        // GET: Tours/Create
        public IActionResult Create()
        {
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiTen");
            return View();
        }

        // POST: Tours/Create
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
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiTen", tour.LoaiId);
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
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiTen", tour.LoaiId);
            return View(tour);
        }

        // POST: Tours/Edit/5
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
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiTen", tour.LoaiId);
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

        // GET: Tour/EditTourChiTiet/5
        public async Task<IActionResult> EditTourChiTiet(int? id)
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
           
            var diaDiem = await context.TourDiaDiem.FindAsync(tourChiTiet.DiaDiemId);
            var tour = await context.Tour.FindAsync(tourChiTiet.TourId);
            ViewData["DiaDiemId"] = diaDiem.DiaDiemTen;
            ViewData["TourId"] = tour.TourTen;
            return View(tourChiTiet);
        }

        // POST: Tour/EditTourChiTiet/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTourChiTiet(int id, [Bind("ChiTietId,ChiTietThuTu,NgayTao")] TourChiTiet tourChiTiet)
        {
            if (id != tourChiTiet.ChiTietId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tct = await context.TourChiTiet.FindAsync(id);
                    tct.ChiTietThuTu = tourChiTiet.ChiTietThuTu;
                    context.Update(tct);
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
                return RedirectToAction(nameof(Details), new { id = tourId });
            }

            var diaDiem = await context.TourDiaDiem.FindAsync(tourChiTiet.DiaDiemId);
            var tour = await context.Tour.FindAsync(tourChiTiet.TourId);
            ViewData["DiaDiemId"] = diaDiem.DiaDiemTen;
            ViewData["TourId"] = tour.TourTen; 
            return View(tourChiTiet);
        }

        private bool TourChiTietExists(int id)
        {
            return context.TourChiTiet.Any(e => e.ChiTietId == id);
        }

        // GET: Tours/DeleteChiTiet/5
        public async Task<IActionResult> DeleteTourChiTiet(int? id)
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

        // POST: Tours/DeleteChiTiet/5
        [HttpPost, ActionName("DeleteTourChiTiet")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTourChiTietConfirmed(int id)
        {
            var tourChiTiet = await context.TourChiTiet.FindAsync(id);
            context.TourChiTiet.Remove(tourChiTiet);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = tourChiTiet.TourId });
        }

        // GET: Tours/CreateChiTiet
        public IActionResult CreateTourChiTiet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = context.Tour.Find(id);
            
            if (tour == null)
            {
                return NotFound();
            }

            ViewData["DiaDiemId"] = new SelectList(context.TourDiaDiem, "DiaDiemId", "DiaDiemTen");
            ViewData["TourId"] = tour.TourTen;

            return View(new TourChiTiet());
        }

        // POST: Tours/CreateChiTiet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTourChiTiet([Bind("ChiTietId,TourId,DiaDiemId,ChiTietThuTu,NgayTao")] TourChiTiet tourChiTiet)
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
    }
}
