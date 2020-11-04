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
    public class TourChiPhisController : Controller
    {
        private readonly TourDBContext _context;

        public TourChiPhisController()
        {
            _context = new TourDBContext();
        }

        // GET: TourChiPhis
        public IActionResult Index(int PageNumber = 1)
        {
            var tourDBContext = _context.TourChiPhi.Include(t => t.Doan).OrderBy(x=>x.Doan.DoanTen);
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            var listTourChiPhi = tourDBContext.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourChiPhi);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, long GiaTu, long GiaDen, int PageNumber = 1)
        {

            IEnumerable<TourChiPhi> listTourChiPhi;
            var tourChiPhis = (from l in _context.TourChiPhi
                                   select l).Include(t => t.Doan).OrderBy(x => x.Doan.DoanTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourChiPhis.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên đoàn") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourChiPhi = tourChiPhis.Where(s => s.Doan.DoanTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourChiPhi.Count() / 5.0);
                return View(listTourChiPhi.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (GiaDen != 0)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.GiaTu = GiaTu;
                ViewBag.GiaDen = GiaDen;
                ViewBag.PageNumber = PageNumber;
                listTourChiPhi = tourChiPhis.Where(s => s.ChiPhiTong > GiaTu && s.ChiPhiTong <= GiaDen);
                ViewBag.TotalPages = Math.Ceiling(listTourChiPhi.Count() / 5.0);
                return View(listTourChiPhi.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }


            return View(tourChiPhis.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: TourChiPhis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhi = await _context.TourChiPhi
                .Include(t => t.Doan)
                .FirstOrDefaultAsync(m => m.ChiPhiId == id);
            if (tourChiPhi == null)
            {
                return NotFound();
            }

            return View(tourChiPhi);
        }

        // GET: TourChiPhis/Create
        public IActionResult Create()
        {
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet");
            return View();
        }

        // POST: TourChiPhis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiPhiId,DoanId,ChiPhiTong,NgayTao")] TourChiPhi tourChiPhi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourChiPhi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", tourChiPhi.DoanId);
            return View(tourChiPhi);
        }

        // GET: TourChiPhis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhi = await _context.TourChiPhi.FindAsync(id);
            if (tourChiPhi == null)
            {
                return NotFound();
            }
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", tourChiPhi.DoanId);
            return View(tourChiPhi);
        }

        // POST: TourChiPhis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiPhiId,DoanId,ChiPhiTong,NgayTao")] TourChiPhi tourChiPhi)
        {
            if (id != tourChiPhi.ChiPhiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourChiPhi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourChiPhiExists(tourChiPhi.ChiPhiId))
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
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", tourChiPhi.DoanId);
            return View(tourChiPhi);
        }

        // GET: TourChiPhis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhi = await _context.TourChiPhi
                .Include(t => t.Doan)
                .FirstOrDefaultAsync(m => m.ChiPhiId == id);
            if (tourChiPhi == null)
            {
                return NotFound();
            }

            return View(tourChiPhi);
        }

        // POST: TourChiPhis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourChiPhi = await _context.TourChiPhi.FindAsync(id);
            _context.TourChiPhi.Remove(tourChiPhi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourChiPhiExists(int id)
        {
            return _context.TourChiPhi.Any(e => e.ChiPhiId == id);
        }
    }
}
