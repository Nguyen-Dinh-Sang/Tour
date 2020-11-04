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
    public class TourChiPhiChiTietsController : Controller
    {
        private readonly TourDBContext _context;

        public TourChiPhiChiTietsController()
        {
            _context = new TourDBContext();
        }

        // GET: TourChiPhiChiTiets
        public IActionResult Index(int PageNumber = 1)
        {
            var tourDBContext = _context.TourChiPhiChiTiet.Include(t => t.ChiPhiNavigation).Include(t => t.LoaiChiPhi).OrderBy(x=>x.LoaiChiPhi.LoaiChiPhiTen);
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            var listTourChiPhiChiTiet = tourDBContext.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourChiPhiChiTiet);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, long GiaTu, long GiaDen, int PageNumber = 1)
        {

            IEnumerable<TourChiPhiChiTiet> listTourChiPhiChiTiet;
            var tourChiPhiChiTiets = (from l in _context.TourChiPhiChiTiet
                               select l).Include(t => t.ChiPhiNavigation).Include(t => t.LoaiChiPhi).OrderBy(x => x.LoaiChiPhi.LoaiChiPhiTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourChiPhiChiTiets.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên loại chi phí") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourChiPhiChiTiet = tourChiPhiChiTiets.Where(s => s.LoaiChiPhi.LoaiChiPhiTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourChiPhiChiTiet.Count() / 5.0);
                return View(listTourChiPhiChiTiet.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (GiaDen != 0)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.GiaTu = GiaTu;
                ViewBag.GiaDen = GiaDen;
                ViewBag.PageNumber = PageNumber;
                listTourChiPhiChiTiet = tourChiPhiChiTiets.Where(s => s.ChiPhi > GiaTu && s.ChiPhi <= GiaDen);
                ViewBag.TotalPages = Math.Ceiling(listTourChiPhiChiTiet.Count() / 5.0);
                return View(listTourChiPhiChiTiet.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }


            return View(tourChiPhiChiTiets.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: TourChiPhiChiTiets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhiChiTiet = await _context.TourChiPhiChiTiet
                .Include(t => t.ChiPhiNavigation)
                .Include(t => t.LoaiChiPhi)
                .FirstOrDefaultAsync(m => m.ChiPhiChiTietId == id);
            if (tourChiPhiChiTiet == null)
            {
                return NotFound();
            }

            return View(tourChiPhiChiTiet);
        }

        // GET: TourChiPhiChiTiets/Create
        public IActionResult Create()
        {
            ViewData["ChiPhiId"] = new SelectList(_context.TourChiPhi, "ChiPhiId", "ChiPhiId");
            ViewData["LoaiChiPhiId"] = new SelectList(_context.TourLoaiChiPhi, "LoaiChiPhiId", "LoaiChiPhiMoTa");
            return View();
        }

        // POST: TourChiPhiChiTiets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiPhiChiTietId,ChiPhiId,LoaiChiPhiId,ChiPhi,NgayTao")] TourChiPhiChiTiet tourChiPhiChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourChiPhiChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChiPhiId"] = new SelectList(_context.TourChiPhi, "ChiPhiId", "ChiPhiId", tourChiPhiChiTiet.ChiPhiId);
            ViewData["LoaiChiPhiId"] = new SelectList(_context.TourLoaiChiPhi, "LoaiChiPhiId", "LoaiChiPhiMoTa", tourChiPhiChiTiet.LoaiChiPhiId);
            return View(tourChiPhiChiTiet);
        }

        // GET: TourChiPhiChiTiets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhiChiTiet = await _context.TourChiPhiChiTiet.FindAsync(id);
            if (tourChiPhiChiTiet == null)
            {
                return NotFound();
            }
            ViewData["ChiPhiId"] = new SelectList(_context.TourChiPhi, "ChiPhiId", "ChiPhiId", tourChiPhiChiTiet.ChiPhiId);
            ViewData["LoaiChiPhiId"] = new SelectList(_context.TourLoaiChiPhi, "LoaiChiPhiId", "LoaiChiPhiMoTa", tourChiPhiChiTiet.LoaiChiPhiId);
            return View(tourChiPhiChiTiet);
        }

        // POST: TourChiPhiChiTiets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiPhiChiTietId,ChiPhiId,LoaiChiPhiId,ChiPhi,NgayTao")] TourChiPhiChiTiet tourChiPhiChiTiet)
        {
            if (id != tourChiPhiChiTiet.ChiPhiChiTietId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourChiPhiChiTiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourChiPhiChiTietExists(tourChiPhiChiTiet.ChiPhiChiTietId))
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
            ViewData["ChiPhiId"] = new SelectList(_context.TourChiPhi, "ChiPhiId", "ChiPhiId", tourChiPhiChiTiet.ChiPhiId);
            ViewData["LoaiChiPhiId"] = new SelectList(_context.TourLoaiChiPhi, "LoaiChiPhiId", "LoaiChiPhiMoTa", tourChiPhiChiTiet.LoaiChiPhiId);
            return View(tourChiPhiChiTiet);
        }

        // GET: TourChiPhiChiTiets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhiChiTiet = await _context.TourChiPhiChiTiet
                .Include(t => t.ChiPhiNavigation)
                .Include(t => t.LoaiChiPhi)
                .FirstOrDefaultAsync(m => m.ChiPhiChiTietId == id);
            if (tourChiPhiChiTiet == null)
            {
                return NotFound();
            }

            return View(tourChiPhiChiTiet);
        }

        // POST: TourChiPhiChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourChiPhiChiTiet = await _context.TourChiPhiChiTiet.FindAsync(id);
            _context.TourChiPhiChiTiet.Remove(tourChiPhiChiTiet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourChiPhiChiTietExists(int id)
        {
            return _context.TourChiPhiChiTiet.Any(e => e.ChiPhiChiTietId == id);
        }
    }
}
