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
        public IActionResult Index(int PageNumber=1)
        {
            var tourLoaiChiPhis = (from l in _context.TourLoaiChiPhi
                                   select l).OrderBy(x => x.LoaiChiPhiTen);
            ViewBag.TotalPages = Math.Ceiling(tourLoaiChiPhis.Count() / 5.0);
            var listTourLoaiChiPhi = tourLoaiChiPhis.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourLoaiChiPhi);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString,long GiaChiPhiTu, long GiaChiPhiDen, int PageNumber = 1)
        {

            IEnumerable<TourLoaiChiPhi> listTourLoaiChiPhi;
            var tourLoaiChiPhis = (from l in _context.TourLoaiChiPhi
                                   select l).OrderBy(x => x.LoaiChiPhiTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourLoaiChiPhis.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourLoaiChiPhi = tourLoaiChiPhis.Where(s => s.LoaiChiPhiTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourLoaiChiPhi.Count() / 5.0);
                return View(listTourLoaiChiPhi.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Mô tả") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourLoaiChiPhi = tourLoaiChiPhis.Where(s => s.LoaiChiPhiTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourLoaiChiPhi.Count() / 5.0);
                return View(listTourLoaiChiPhi.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (GiaChiPhiDen!=0)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.GiaChiPhiTu = GiaChiPhiTu;
                ViewBag.GiaChiPhiDen = GiaChiPhiDen;
                ViewBag.PageNumber = PageNumber;
                listTourLoaiChiPhi = tourLoaiChiPhis.Where(s => s.LoaiChiPhiSoTien > GiaChiPhiTu && s.LoaiChiPhiSoTien <= GiaChiPhiDen);
                ViewBag.TotalPages = Math.Ceiling(listTourLoaiChiPhi.Count() / 5.0);
                return View(listTourLoaiChiPhi.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
                

            return View(tourLoaiChiPhis.Skip((PageNumber - 1) * 5).Take(5).ToList());
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
        public async Task<IActionResult> Create([Bind("LoaiChiPhiId,LoaiChiPhiTen,LoaiChiPhiMoTa,LoaiChiPhiSoTien,NgayTao")] TourLoaiChiPhi tourLoaiChiPhi)
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
        public async Task<IActionResult> Edit(int id, [Bind("LoaiChiPhiId,LoaiChiPhiTen,LoaiChiPhiMoTa,LoaiChiPhiSoTien,NgayTao")] TourLoaiChiPhi tourLoaiChiPhi)
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
