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
    public class GiaTourHienTaisController : Controller
    {
        private readonly TourDBContext context;

        public GiaTourHienTaisController()
        {
            context = new TourDBContext();
        }

        // GET: GiaTourHienTais
        public IActionResult Index(int PageNumber = 1)
        {
            var tourDBContext =context.GiaTourHienTai.Include(g => g.Gia).Include(g => g.Tour);
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            var listTourGiaHienTai = tourDBContext.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourGiaHienTai);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, long GiaTu, long GiaDen, int PageNumber = 1)
        {

            IEnumerable<GiaTourHienTai> listTourGiaHienTai;
          
            var tourDBContext = context.GiaTourHienTai.Include(g => g.Gia).Include(g => g.Tour).OrderBy(x => x.Tour.TourTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên tour") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourGiaHienTai = tourDBContext.Where(s => s.Tour.TourTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourGiaHienTai.Count() / 5.0);
                return View(listTourGiaHienTai.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (GiaDen!=0)
            {
                ViewBag.GiaTourTu = GiaTu ;
                ViewBag.GiaTourDen =GiaDen;
                ViewBag.PageNumber = PageNumber;
                listTourGiaHienTai = tourDBContext.Where(s => s.Gia.GiaSoTien>GiaTu && s.Gia.GiaSoTien<=GiaDen);
                ViewBag.TotalPages = Math.Ceiling(listTourGiaHienTai.Count() / 5.0);
                return View(listTourGiaHienTai.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            return View(tourDBContext.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: GiaTourHienTais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaTourHienTai = await context.GiaTourHienTai
                .Include(g => g.Gia)
                .Include(g => g.Tour)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (giaTourHienTai == null)
            {
                return NotFound();
            }

            return View(giaTourHienTai);
        }
        public IActionResult Jquery(int ?id)
        {
            Console.WriteLine(id);
            var tourGia = from tg in context.TourGia
                          where tg.TourId.Equals(id)
                          select tg;
            foreach (var c in tourGia) Console.WriteLine(c.TourId);
            return new JsonResult(tourGia);

            
        }
        // GET: GiaTourHienTais/Create
        public IActionResult Create()
        {
            var Tour = from t in context.Tour
                       where !((from gtht in context.GiaTourHienTai
                                select gtht.TourId).Contains(t.TourId))
                       select t;
            foreach (var c in Tour) Console.WriteLine(c.GiaTourHienTai);
            ViewData["GiaId"] = new SelectList(context.TourGia, "GiaId", "GiaTuNgay");
            ViewData["TourId"] = new SelectList(Tour, "TourId", "TourTen");
            return View();
        }

        // POST: GiaTourHienTais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourId,GiaId,NgayTao")] GiaTourHienTai giaTourHienTai)
        {
            if (ModelState.IsValid)
            {
                context.Add(giaTourHienTai);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GiaId"] = new SelectList(context.TourGia, "GiaId", "GiaTuNgay", giaTourHienTai.GiaId);
            ViewData["TourId"] = new SelectList(context.Tour, "TourId", "TourTen", giaTourHienTai.TourId);
            return View(giaTourHienTai);
        }

        // GET: GiaTourHienTais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaTourHienTai = await context.GiaTourHienTai.FindAsync(id);
            if (giaTourHienTai == null)
            {
                return NotFound();
            }
            ViewData["GiaId"] = new SelectList(context.TourGia, "GiaId", "GiaTuNgay", giaTourHienTai.GiaId);
            var tour = await context.Tour.FindAsync(giaTourHienTai.TourId);
            ViewData["TourId"] = tour.TourTen;
            ViewBag.editTourId = id;
            return View(giaTourHienTai);
        }

        // POST: GiaTourHienTais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GiaId,NgayTao")] GiaTourHienTai giaTourHienTai)
        {
            giaTourHienTai.TourId = id;

            if (id != giaTourHienTai.TourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(giaTourHienTai);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiaTourHienTaiExists(giaTourHienTai.TourId))
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
            ViewData["GiaId"] = new SelectList(context.TourGia, "GiaId", "GiaTuNgay", giaTourHienTai.GiaId);
            var tour = await context.Tour.FindAsync(giaTourHienTai.TourId);
            ViewData["TourId"] = tour.TourTen;
            return View(giaTourHienTai);
        }

        // GET: GiaTourHienTais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaTourHienTai = await context.GiaTourHienTai
                .Include(g => g.Gia)
                .Include(g => g.Tour)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (giaTourHienTai == null)
            {
                return NotFound();
            }

            return View(giaTourHienTai);
        }

        // POST: GiaTourHienTais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giaTourHienTai = await context.GiaTourHienTai.FindAsync(id);
            context.GiaTourHienTai.Remove(giaTourHienTai);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiaTourHienTaiExists(int id)
        {
            return context.GiaTourHienTai.Any(e => e.TourId == id);
        }
    }
}
