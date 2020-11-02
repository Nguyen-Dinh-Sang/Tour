﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourMVC.Models;

namespace TourMVC.Controllers
{
    public class TourChiTietsController : Controller
    {
        private readonly TourDBContext _context;

        public TourChiTietsController()
        {
            _context = new TourDBContext();
        }

        // GET: TourChiTiets
        public async Task<IActionResult> Index()
        {
            var tourDBContext = _context.TourChiTiet.Include(t => t.DiaDiem).Include(t => t.Tour);
            return View(await tourDBContext.ToListAsync());
        }

        // GET: TourChiTiets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiTiet = await _context.TourChiTiet
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
            ViewData["DiaDiemId"] = new SelectList(_context.TourDiaDiem, "DiaDiemId", "DiaDiemMoTa");
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa");
            return View();
        }

        // POST: TourChiTiets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiTietId,TourId,DiaDiemId,ChiTietThuTu,NgayTao")] TourChiTiet tourChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiaDiemId"] = new SelectList(_context.TourDiaDiem, "DiaDiemId", "DiaDiemMoTa", tourChiTiet.DiaDiemId);
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourChiTiet.TourId);
            return View(tourChiTiet);
        }

        // GET: TourChiTiets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiTiet = await _context.TourChiTiet.FindAsync(id);
            if (tourChiTiet == null)
            {
                return NotFound();
            }
            ViewData["DiaDiemId"] = new SelectList(_context.TourDiaDiem, "DiaDiemId", "DiaDiemMoTa", tourChiTiet.DiaDiemId);
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourChiTiet.TourId);
            return View(tourChiTiet);
        }

        // POST: TourChiTiets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(tourChiTiet);
                    await _context.SaveChangesAsync();
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
            ViewData["DiaDiemId"] = new SelectList(_context.TourDiaDiem, "DiaDiemId", "DiaDiemMoTa", tourChiTiet.DiaDiemId);
            ViewData["TourId"] = new SelectList(_context.Tour, "TourId", "TourMoTa", tourChiTiet.TourId);
            return View(tourChiTiet);
        }

        // GET: TourChiTiets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiTiet = await _context.TourChiTiet
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
            var tourChiTiet = await _context.TourChiTiet.FindAsync(id);
            _context.TourChiTiet.Remove(tourChiTiet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourChiTietExists(int id)
        {
            return _context.TourChiTiet.Any(e => e.ChiTietId == id);
        }
    }
}
