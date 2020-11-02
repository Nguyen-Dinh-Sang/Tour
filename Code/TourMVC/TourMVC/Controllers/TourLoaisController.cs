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
        private readonly TourDBContext _context;

        public TourLoaisController()
        {
            _context = new TourDBContext();
        }

        // GET: TourLoais
        public async Task<IActionResult> Index()
        {
            return View(await _context.TourLoai.ToListAsync());
        }

        // GET: TourLoais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoai = await _context.TourLoai
                .FirstOrDefaultAsync(m => m.LoaiId == id);
            if (tourLoai == null)
            {
                return NotFound();
            }

            return View(tourLoai);
        }

        // GET: TourLoais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TourLoais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiId,LoaiTen,LoaiMoTa,NgayTao")] TourLoai tourLoai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourLoai);
                await _context.SaveChangesAsync();
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

            var tourLoai = await _context.TourLoai.FindAsync(id);
            if (tourLoai == null)
            {
                return NotFound();
            }
            return View(tourLoai);
        }

        // POST: TourLoais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(tourLoai);
                    await _context.SaveChangesAsync();
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

            var tourLoai = await _context.TourLoai
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
            var tourLoai = await _context.TourLoai.FindAsync(id);
            _context.TourLoai.Remove(tourLoai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourLoaiExists(int id)
        {
            return _context.TourLoai.Any(e => e.LoaiId == id);
        }
    }
}
