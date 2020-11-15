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
    public class TourNhanViensController : Controller
    {
        private readonly TourDBContext context;

        public TourNhanViensController()
        {
            context = new TourDBContext();
        }

        public IActionResult Index(int PageNumber = 1)
        {
            var tourNhanViens = (from l in context.TourNhanVien
                                 select l).OrderBy(x => x.NhanVienTen);
            ViewBag.TotalPages = Math.Ceiling(tourNhanViens.Count() / 5.0);
            var listTourNhanVien = tourNhanViens.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourNhanVien);
        }

        [HttpGet]
        public IActionResult Index(string classify, string searchString, int PageNumber = 1)
        {

            IEnumerable<TourNhanVien> listTourNhanVien;
            var tourNhanViens = (from l in context.TourNhanVien
                                 select l).OrderBy(x => x.NhanVienTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourNhanViens.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên nhân viên") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourNhanVien = tourNhanViens.Where(s => s.NhanVienTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourNhanVien.Count() / 5.0);
                return View(listTourNhanVien.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Số điện thoại") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourNhanVien = tourNhanViens.Where(s => s.NhanVienSoDienThoai.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourNhanVien.Count() / 5.0);
                return View(listTourNhanVien.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Email") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourNhanVien = tourNhanViens.Where(s => s.NhanVienEmail.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourNhanVien.Count() / 5.0);
                return View(listTourNhanVien.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            return View(tourNhanViens.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourNhanVien = await context.TourNhanVien
                .FirstOrDefaultAsync(m => m.NhanVienId == id);

            context.Entry(tourNhanVien).Collection(nv => nv.DoanNhanVien).Query().Include(dnv => dnv.Doan).Include(d => d.Doan.Tour).OrderBy(dnv => dnv.NgayTao).Load();

            if (tourNhanVien == null)
            {
                return NotFound();
            }

            return View(tourNhanVien);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhanVienId,NhanVienTen,NhanVienSoDienThoai,NhanVienEmail,NhanVienNgaySinh,NgayTao")] TourNhanVien tourNhanVien)
        {
            if (ModelState.IsValid)
            {
                context.Add(tourNhanVien);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourNhanVien);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourNhanVien = await context.TourNhanVien.FindAsync(id);
            if (tourNhanVien == null)
            {
                return NotFound();
            }
            return View(tourNhanVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NhanVienId,NhanVienTen,NhanVienSoDienThoai,NhanVienEmail,NhanVienNgaySinh,NgayTao")] TourNhanVien tourNhanVien)
        {
            if (id != tourNhanVien.NhanVienId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tourNhanVien);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourNhanVienExists(tourNhanVien.NhanVienId))
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
            return View(tourNhanVien);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourNhanVien = await context.TourNhanVien
                .FirstOrDefaultAsync(m => m.NhanVienId == id);
            if (tourNhanVien == null)
            {
                return NotFound();
            }

            return View(tourNhanVien);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourNhanVien = await context.TourNhanVien.FindAsync(id);
            context.TourNhanVien.Remove(tourNhanVien);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourNhanVienExists(int id)
        {
            return context.TourNhanVien.Any(e => e.NhanVienId == id);
        }
    }
}
