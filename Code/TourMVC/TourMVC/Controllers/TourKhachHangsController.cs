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
    public class TourKhachHangsController : Controller
    {
        private readonly TourDBContext context;

        public TourKhachHangsController()
        {
            context = new TourDBContext();
        }

        // GET: TourKhachHangs
        public IActionResult Index(int PageNumber = 1)
        {
            var tourKhachHangs = (from l in context.TourKhachHang
                                select l).OrderBy(x => x.KhachHangTen);
            ViewBag.TotalPages = Math.Ceiling(tourKhachHangs.Count() / 5.0);
            var listTourKhachHang = tourKhachHangs.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourKhachHang);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, int PageNumber = 1)
        {

            IEnumerable<TourKhachHang> listTourKhachHang;
            var tourKhachHangs = (from l in context.TourKhachHang
                                select l).OrderBy(x => x.KhachHangTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourKhachHangs.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên khách hàng") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourKhachHang = tourKhachHangs.Where(s => s.KhachHangTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourKhachHang.Count() / 5.0);
                return View(listTourKhachHang.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Số điện thoại") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourKhachHang = tourKhachHangs.Where(s => s.KhachHangSoDienThoai.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourKhachHang.Count() / 5.0);
                return View(listTourKhachHang.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Email") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourKhachHang = tourKhachHangs.Where(s => s.KhachHangEmail.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourKhachHang.Count() / 5.0);
                return View(listTourKhachHang.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Số chứng minh nhân dân") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourKhachHang = tourKhachHangs.Where(s => s.KhachHangChungMinhNhanDan.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourKhachHang.Count() / 5.0);
                return View(listTourKhachHang.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            return View(tourKhachHangs.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: TourKhachHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourKhachHang = await context.TourKhachHang
                .FirstOrDefaultAsync(m => m.KhachHangId == id);
            if (tourKhachHang == null)
            {
                return NotFound();
            }

            return View(tourKhachHang);
        }

        // GET: TourKhachHangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TourKhachHangs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KhachHangId,KhachHangTen,KhachHangSoDienThoai,KhachHangEmail,KhachHangNgaySinh,KhachHangChungMinhNhanDan,NgayTao")] TourKhachHang tourKhachHang)
        {
            if (ModelState.IsValid)
            {
                context.Add(tourKhachHang);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourKhachHang);
        }

        // GET: TourKhachHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourKhachHang = await context.TourKhachHang.FindAsync(id);
            if (tourKhachHang == null)
            {
                return NotFound();
            }
            return View(tourKhachHang);
        }

        // POST: TourKhachHangs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KhachHangId,KhachHangTen,KhachHangSoDienThoai,KhachHangEmail,KhachHangNgaySinh,KhachHangChungMinhNhanDan,NgayTao")] TourKhachHang tourKhachHang)
        {
            if (id != tourKhachHang.KhachHangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tourKhachHang);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourKhachHangExists(tourKhachHang.KhachHangId))
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
            return View(tourKhachHang);
        }

        // GET: TourKhachHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourKhachHang = await context.TourKhachHang
                .FirstOrDefaultAsync(m => m.KhachHangId == id);
            if (tourKhachHang == null)
            {
                return NotFound();
            }

            return View(tourKhachHang);
        }

        // POST: TourKhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourKhachHang = await context.TourKhachHang.FindAsync(id);
            context.TourKhachHang.Remove(tourKhachHang);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourKhachHangExists(int id)
        {
            return context.TourKhachHang.Any(e => e.KhachHangId == id);
        }
    }
}
