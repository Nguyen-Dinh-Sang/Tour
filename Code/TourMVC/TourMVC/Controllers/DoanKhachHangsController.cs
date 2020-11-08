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
    public class DoanKhachHangsController : Controller
    {
        private readonly TourDBContext context;

        public DoanKhachHangsController()
        {
            context = new TourDBContext();
        }

        // GET: DoanKhachHangs
        public IActionResult Index(int PageNumber = 1)
        {
            var tourDBContext = context.DoanKhachHang.Include(d => d.Doan).Include(d => d.KhachHang).OrderBy(x=>x.Doan.DoanTen);
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            var listTourDoanKhachHang = tourDBContext.Skip((PageNumber - 1) * 5).Take(5);
            return View(listTourDoanKhachHang.ToListAsync());
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, int PageNumber = 1)
        {

            IEnumerable<DoanKhachHang> listTourDoanKhachHang;
            var tourDoanKhachHangs = (from l in context.DoanKhachHang
                                select l).Include(d => d.Doan).Include(d => d.KhachHang).OrderBy(x => x.Doan.DoanTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourDoanKhachHangs.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên đoàn") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourDoanKhachHang = tourDoanKhachHangs.Where(s => s.Doan.DoanTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourDoanKhachHang.Count() / 5.0);
                return View(listTourDoanKhachHang.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên khách hàng") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourDoanKhachHang = tourDoanKhachHangs.Where(s => s.KhachHang.KhachHangTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourDoanKhachHang.Count() / 5.0);
                return View(listTourDoanKhachHang.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            return View(tourDoanKhachHangs.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: DoanKhachHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanKhachHang = await context.DoanKhachHang
                .Include(d => d.Doan)
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(m => m.DoanKhachHangId == id);
            if (doanKhachHang == null)
            {
                return NotFound();
            }

            return View(doanKhachHang);
        }

        // GET: DoanKhachHangs/Create
        public IActionResult Create()
        {
            ViewData["DoanId"] = new SelectList(context.TourDoan, "DoanId", "DoanChiTiet");
            ViewData["KhachHangId"] = new SelectList(context.TourKhachHang, "KhachHangId", "KhachHangChungMinhNhanDan");
            return View();
        }

        // POST: DoanKhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoanKhachHangId,KhachHangId,DoanId,NgayTao")] DoanKhachHang doanKhachHang)
        {
            if (ModelState.IsValid)
            {
                context.Add(doanKhachHang);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoanId"] = new SelectList(context.TourDoan, "DoanId", "DoanChiTiet", doanKhachHang.DoanId);
            ViewData["KhachHangId"] = new SelectList(context.TourKhachHang, "KhachHangId", "KhachHangChungMinhNhanDan", doanKhachHang.KhachHangId);
            return View(doanKhachHang);
        }

        // GET: DoanKhachHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanKhachHang = await context.DoanKhachHang.FindAsync(id);
            if (doanKhachHang == null)
            {
                return NotFound();
            }
            ViewData["DoanId"] = new SelectList(context.TourDoan, "DoanId", "DoanChiTiet", doanKhachHang.DoanId);
            ViewData["KhachHangId"] = new SelectList(context.TourKhachHang, "KhachHangId", "KhachHangChungMinhNhanDan", doanKhachHang.KhachHangId);
            return View(doanKhachHang);
        }

        // POST: DoanKhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoanKhachHangId,KhachHangId,DoanId,NgayTao")] DoanKhachHang doanKhachHang)
        {
            if (id != doanKhachHang.DoanKhachHangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(doanKhachHang);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoanKhachHangExists(doanKhachHang.DoanKhachHangId))
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
            ViewData["DoanId"] = new SelectList(context.TourDoan, "DoanId", "DoanChiTiet", doanKhachHang.DoanId);
            ViewData["KhachHangId"] = new SelectList(context.TourKhachHang, "KhachHangId", "KhachHangChungMinhNhanDan", doanKhachHang.KhachHangId);
            return View(doanKhachHang);
        }

        // GET: DoanKhachHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanKhachHang = await context.DoanKhachHang
                .Include(d => d.Doan)
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(m => m.DoanKhachHangId == id);
            if (doanKhachHang == null)
            {
                return NotFound();
            }

            return View(doanKhachHang);
        }

        // POST: DoanKhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doanKhachHang = await context.DoanKhachHang.FindAsync(id);
            context.DoanKhachHang.Remove(doanKhachHang);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoanKhachHangExists(int id)
        {
            return context.DoanKhachHang.Any(e => e.DoanKhachHangId == id);
        }
    }
}
