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
    public class TourLoaisController : Controller
    {
        private readonly TourDBContext context;
        private static int? loaiTourId;

        public TourLoaisController()
        {
            context = new TourDBContext();
        }

        public IActionResult Index(int PageNumber = 1)
        {
            var tourLoais = (from l in context.TourLoai
                             select l).OrderBy(x => x.LoaiTen);
            ViewBag.TotalPages = Math.Ceiling(tourLoais.Count() / 5.0);
            var listTourLoai = tourLoais.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourLoai);
        }

        [HttpGet]
        public IActionResult Index(string classify, string searchString, int PageNumber = 1)
        {

            IEnumerable<TourLoai> listTourLoai;
            var tourLoais = (from l in context.TourLoai
                             select l).OrderBy(x => x.LoaiTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourLoais.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên loại") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourLoai = tourLoais.Where(s => s.LoaiTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourLoai.Count() / 5.0);
                return View(listTourLoai.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Mô tả loại") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourLoai = tourLoais.Where(s => s.LoaiMoTa.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourLoai.Count() / 5.0);
                return View(listTourLoai.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }

            return View(tourLoais.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoai = await context.TourLoai
                .FirstOrDefaultAsync(m => m.LoaiId == id);
            context.Entry(tourLoai).Collection(tl => tl.Tour).Query().OrderBy(t => t.NgayTao).Load();
            if (tourLoai == null)
            {
                return NotFound();
            }
            loaiTourId = id;
            return View(tourLoai);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiId,LoaiTen,LoaiMoTa,NgayTao")] TourLoai tourLoai)
        {
            if (ModelState.IsValid)
            {
                context.Add(tourLoai);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourLoai);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoai = await context.TourLoai.FindAsync(id);
            if (tourLoai == null)
            {
                return NotFound();
            }
            return View(tourLoai);
        }

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
                    context.Update(tourLoai);
                    await context.SaveChangesAsync();
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourLoai = await context.TourLoai
                .FirstOrDefaultAsync(m => m.LoaiId == id);
            if (tourLoai == null)
            {
                return NotFound();
            }

            return View(tourLoai);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourLoai = await context.TourLoai.FindAsync(id);
            context.TourLoai.Remove(tourLoai);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourLoaiExists(int id)
        {
            return context.TourLoai.Any(e => e.LoaiId == id);
        }

        public async Task<IActionResult> EditTour(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTour(int id, [Bind("TourId,TourTen,TourMoTa,LoaiId,NgayTao")] Tour tour)
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
                return RedirectToAction(nameof(Details), new { id = loaiTourId });
            }
            ViewData["LoaiId"] = new SelectList(context.TourLoai, "LoaiId", "LoaiTen", tour.LoaiId);
            return View(tour);
        }

        private bool TourExists(int id)
        {
            return context.Tour.Any(e => e.TourId == id);
        }

        public async Task<IActionResult> DeleteTour(int? id)
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

        [HttpPost, ActionName("DeleteTour")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTourConfirmed(int id)
        {
            var tour = await context.Tour.FindAsync(id);
            context.Tour.Remove(tour);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = tour.LoaiId });
        }
    }
}
