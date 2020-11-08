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
    public class TourDoansController : Controller
    {
        private readonly TourDBContext context;
        private static int? doanId;

        public TourDoansController()
        {
            context = new TourDBContext();
        }

        // GET: TourDoans
        public IActionResult Index(int PageNumber = 1)
        {
            var tourDBContext = context.TourDoan.Include(t => t.Tour).OrderBy(x=>x.DoanTen);
            ViewBag.TotalPages = Math.Ceiling(tourDBContext.Count() / 5.0);
            var listTourDoan = tourDBContext.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(listTourDoan);
        }
        [HttpGet]
        public IActionResult Index(string classify, string searchString, int PageNumber = 1)
        {

            IEnumerable<TourDoan> listTourDoan;
            var tourDoans = (from l in context.TourDoan
                                   select l).Include(t=>t.Tour).OrderBy(x => x.DoanTen);
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling(tourDoans.Count() / 5.0);
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên đoàn") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourDoan = tourDoans.Where(s => s.DoanTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourDoan.Count() / 5.0);
                return View(listTourDoan.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Chi tiết đoàn") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourDoan = tourDoans.Where(s => s.DoanChiTiet.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourDoan.Count() / 5.0);
                return View(listTourDoan.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }
            if (!String.IsNullOrEmpty(searchString) && classify.Contains("Tên tour") == true)
            {
                ViewBag.searchString = searchString;
                ViewBag.classify = classify;
                ViewBag.PageNumber = PageNumber;
                listTourDoan = tourDoans.Where(s => s.Tour.TourTen.Contains(searchString));
                ViewBag.TotalPages = Math.Ceiling(listTourDoan.Count() / 5.0);
                return View(listTourDoan.Skip((PageNumber - 1) * 5).Take(5).ToList());
            }


            return View(tourDoans.Skip((PageNumber - 1) * 5).Take(5).ToList());
        }
        // GET: TourDoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDoan = await context.TourDoan
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.DoanId == id);
            context.Entry(tourDoan).Collection(td => td.DoanKhachHang).Query().Include(dkh => dkh.KhachHang).OrderBy(dkh => dkh.NgayTao).Load();
            context.Entry(tourDoan).Collection(td => td.DoanNhanVien).Query().Include(dnv => dnv.NhanVien).OrderBy(dnv => dnv.NgayTao).Load();
            if (tourDoan == null)
            {
                return NotFound();
            }
            doanId = id;
            return View(tourDoan);
        }

        // GET: TourDoans/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(context.Tour, "TourId", "TourTen");
            return View();
        }

        // POST: TourDoans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoanId,TourId,DoanTen,DoanNgayDi,DoanNgayVe,DoanChiTiet,DoanGiaTour,NgayTao")] TourDoan tourDoan)
        {
            if (ModelState.IsValid)
            {
                var giaTourHienTai = context.GiaTourHienTai.Where(gtht => gtht.TourId == tourDoan.TourId).Include(gt => gt.Gia).FirstOrDefault();
                tourDoan.DoanGiaTour = giaTourHienTai.Gia.GiaSoTien;
                context.Add(tourDoan);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(context.Tour, "TourId", "TourTen", tourDoan.TourId);
            return View(tourDoan);
        }

        // GET: TourDoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDoan = await context.TourDoan.FindAsync(id);
            if (tourDoan == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(context.Tour, "TourId", "TourTen", tourDoan.TourId);
            return View(tourDoan);
        }

        // POST: TourDoans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoanId,TourId,DoanTen,DoanNgayDi,DoanNgayVe,DoanChiTiet,DoanGiaTour,NgayTao")] TourDoan tourDoan)
        {
            if (id != tourDoan.DoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var giaTourHienTai = context.GiaTourHienTai.Where(gtht => gtht.TourId == tourDoan.TourId).Include(gt => gt.Gia).FirstOrDefault();
                    tourDoan.DoanGiaTour = giaTourHienTai.Gia.GiaSoTien;
                    context.Update(tourDoan);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourDoanExists(tourDoan.DoanId))
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
            ViewData["TourId"] = new SelectList(context.Tour, "TourId", "TourTen", tourDoan.TourId);
            return View(tourDoan);
        }

        // GET: TourDoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDoan = await context.TourDoan
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.DoanId == id);
            if (tourDoan == null)
            {
                return NotFound();
            }

            return View(tourDoan);
        }

        // POST: TourDoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourDoan = await context.TourDoan.FindAsync(id);
            context.TourDoan.Remove(tourDoan);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourDoanExists(int id)
        {
            return context.TourDoan.Any(e => e.DoanId == id);
        }

        // GET: TourDoans/AddKhachHang
        public IActionResult AddKhachHang(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doan = context.TourDoan.Find(id);

            if (doan == null)
            {
                return NotFound();
            }

            ViewData["DoanId"] = doan.DoanTen;
            ViewData["KhachHangId"] = new SelectList(context.TourKhachHang, "KhachHangId", "KhachHangTen");

            DoanKhachHang doanKhach = new DoanKhachHang();
            doanKhach.DoanId = doan.DoanId;
            
            return View(doanKhach);
        }

        // POST: TourDoans/AddKhachHang
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddKhachHang([Bind("DoanKhachHangId,KhachHangId,NgayTao")] DoanKhachHang doanKhachHang)
        {
            if (ModelState.IsValid)
            {
                doanKhachHang.DoanId = (int) doanId;
                context.Add(doanKhachHang);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = doanKhachHang.DoanId });
            }
            ViewData["DoanId"] = new SelectList(context.TourDoan, "DoanId", "DoanChiTiet", doanKhachHang.DoanId);
            ViewData["KhachHangId"] = new SelectList(context.TourKhachHang, "KhachHangId", "KhachHangTen", doanKhachHang.KhachHangId);
            return View(doanKhachHang);
        }
    }
}
