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
    public class TourGiasController : Controller
    {
        private readonly TourDBContext _context;

        public TourGiasController()
        {
            _context = new TourDBContext();
        }

        // GET: TourGias
        public IActionResult Index(int PageNumber = 1)
        {
            var tourDBContext = _context.TourGia.Include(t => t.Tour).OrderBy(x=>x.Tour.TourTen);
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            var listTourGia = tourDBContext.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourGia);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, long GiaTu, long GiaDen, int PageNumber = 1)
        {

            IEnumerable<TourGia> listTourGia;
            var tourGias = (from l in _context.TourGia
                                   select l).Include(t => t.Tour).OrderBy(x => x.Tour.TourTen);
            var tourGiaNotUse = from tgnu in _context.TourGia
                                where !((from tght in _context.GiaTourHienTai
                                        select tght.GiaId).Contains(tgnu.GiaId))
                                select tgnu;
            ViewBag.tourGiaNotUse = tourGiaNotUse.ToList();
            ViewBag.tourGiaTenTour = _context.Tour;
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourGias.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên tour") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourGia = tourGias.Where(s => s.Tour.TourTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourGia.Count() / 5.0);
                return View(listTourGia.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (GiaDen != 0)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.GiaTu = GiaTu;
                ViewBag.GiaDen = GiaDen;
                ViewBag.PageNumber = PageNumber;
                listTourGia = tourGias.Where(s => s.GiaSoTien > GiaTu && s.GiaSoTien <= GiaDen);
                ViewBag.TotalPages = Math.Ceiling(listTourGia.Count() / 5.0);
                return View(listTourGia.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }


            return View(tourGias.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: TourGias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourGia = await _context.TourGia
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.GiaId == id);
            if (tourGia == null)
            {
                return NotFound();
            }

            return View(tourGia);
        }

        // GET: TourGias/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourTen");
            return View();
        }

        // POST: TourGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GiaId,GiaSoTien,TourId,GiaTuNgay,GiaDenNgay,NgayTao")] TourGia tourGia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourTen", tourGia.TourId);
            return View(tourGia);
        }

        // GET: TourGias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourGia = await _context.TourGia.FindAsync(id);
            if (tourGia == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourGia.TourId);
            return View(tourGia);
        }

        // POST: TourGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GiaId,GiaSoTien,TourId,GiaTuNgay,GiaDenNgay,NgayTao")] TourGia tourGia)
        {
            if (id != tourGia.GiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourGiaExists(tourGia.GiaId))
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
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourGia.TourId);
            return View(tourGia);
        }

        // GET: TourGias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourGia = await _context.TourGia
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.GiaId == id);
            if (tourGia == null)
            {
                return NotFound();
            }

            return View(tourGia);
        }

        // POST: TourGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourGia = await _context.TourGia.FindAsync(id);
            _context.TourGia.Remove(tourGia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourGiaExists(int id)
        {
            return _context.TourGia.Any(e => e.GiaId == id);
        }
    }
}
