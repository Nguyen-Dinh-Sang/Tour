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
            context.Entry(tourDoan).Collection(td => td.TourChiPhiChiTiet).Query().Include(tcpct => tcpct.LoaiChiPhi).OrderBy(tcpct => tcpct.NgayTao).Load();

            ViewData["TongChiPhi"] = context.TourChiPhiChiTiet.Where(tcpct => tcpct.DoanId == id).Sum(i => i.ChiPhi);

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
            var doan = context.TourDoan.Find(doanKhachHang.DoanId);

            if (doan == null)
            {
                return NotFound();
            }

            ViewData["DoanId"] = doan.DoanTen;
            ViewData["KhachHangId"] = new SelectList(context.TourKhachHang, "KhachHangId", "KhachHangTen", doanKhachHang.KhachHangId);
            return View(doanKhachHang);
        }


        public async Task<IActionResult> DetailsKhachHang(int? id)
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

            ViewData["DoanId"] = doanId;
            return View(tourKhachHang);
        }

        // GET: TourDoans/DeleteKhachHang/5
        public async Task<IActionResult> DeleteKhachHang(int? id)
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

        // POST:  TourDoans/DeleteKhachHang/5
        [HttpPost, ActionName("DeleteKhachHang")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            var doanKhachHang = await context.DoanKhachHang.FindAsync(id);
            context.DoanKhachHang.Remove(doanKhachHang);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = doanKhachHang.DoanId });
        }

        public IActionResult AddNhanVien(int id)
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
            ViewData["NhanVienId"] = new SelectList(context.TourNhanVien, "NhanVienId", "NhanVienTen");

            DoanNhanVien doanNhanVien = new DoanNhanVien();
            doanNhanVien.DoanId = doan.DoanId;
            return View(doanNhanVien);
        }

        // POST: DoanNhanViens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNhanVien([Bind("DoanNhanVienId,NhanVienId,NhanVienNhiemVu,NgayTao")] DoanNhanVien doanNhanVien)
        {
            if (ModelState.IsValid)
            {
                doanNhanVien.DoanId = (int)doanId;
                context.Add(doanNhanVien);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = doanNhanVien.DoanId });
            }

            var doan = context.TourDoan.Find(doanNhanVien.DoanId);

            if (doan == null)
            {
                return NotFound();
            }

            ViewData["DoanId"] = doan.DoanTen;
            ViewData["NhanVienId"] = new SelectList(context.TourNhanVien, "NhanVienId", "NhanVienTen", doanNhanVien.NhanVienId);
            return View(doanNhanVien);
        }

        public async Task<IActionResult> EditNhanVien(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanNhanVien = await context.DoanNhanVien.Include(dnv => dnv.Doan).Include(dnv => dnv.NhanVien).FirstOrDefaultAsync(dnv => dnv.DoanNhanVienId == id);
            if (doanNhanVien == null)
            {
                return NotFound();
            }
            ViewData["DoanId"] = doanNhanVien.Doan.DoanTen;
            ViewData["NhanVienId"] = doanNhanVien.NhanVien.NhanVienTen;
            return View(doanNhanVien);
        }

        // POST: TourDoans/EditNhanVien/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNhanVien(int id, [Bind("DoanNhanVienId,NhanVienNhiemVu,NgayTao")] DoanNhanVien doanNhanVien)
        {
            if (id != doanNhanVien.DoanNhanVienId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dnv = await context.DoanNhanVien.FindAsync(id);
                    dnv.NhanVienNhiemVu = doanNhanVien.NhanVienNhiemVu;
                    context.Update(dnv);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoanNhanVienExists(doanNhanVien.DoanNhanVienId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = doanId });
            }

            doanNhanVien = await context.DoanNhanVien.Include(dnv => dnv.Doan).Include(dnv => dnv.NhanVien).FirstOrDefaultAsync(dnv => dnv.DoanNhanVienId == id);
            if (doanNhanVien == null)
            {
                return NotFound();
            }
            ViewData["DoanId"] = doanNhanVien.Doan.DoanTen;
            ViewData["NhanVienId"] = doanNhanVien.NhanVien.NhanVienTen; 
            return View(doanNhanVien);
        }

        private bool DoanNhanVienExists(int id)
        {
            return context.DoanNhanVien.Any(e => e.DoanNhanVienId == id);
        }

        public async Task<IActionResult> DetailsNhanVien(int? id)
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

            ViewData["DoanId"] = doanId;
            return View(tourNhanVien);
        }

        public async Task<IActionResult> DeleteNhanVien(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanNhanVien = await context.DoanNhanVien
                .Include(d => d.Doan)
                .Include(d => d.NhanVien)
                .FirstOrDefaultAsync(m => m.DoanNhanVienId == id);
            if (doanNhanVien == null)
            {
                return NotFound();
            }

            return View(doanNhanVien);
        }

        [HttpPost, ActionName("DeleteNhanVien")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNhanVienConfirmed(int id)
        {
            var doanNhanVien = await context.DoanNhanVien.FindAsync(id);
            context.DoanNhanVien.Remove(doanNhanVien);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = doanNhanVien.DoanId });
        }

        public IActionResult AddChiPhiChiTiet(int id)
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
            ViewData["LoaiChiPhiId"] = new SelectList(context.TourLoaiChiPhi, "LoaiChiPhiId", "LoaiChiPhiTen");

            TourChiPhiChiTiet doanNhanVien = new TourChiPhiChiTiet();
            doanNhanVien.DoanId = doan.DoanId;
            return View(doanNhanVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChiPhiChiTiet([Bind("ChiPhiChiTietId,LoaiChiPhiId,ChiPhi,NgayTao")] TourChiPhiChiTiet tourChiPhiChiTiet)
        {
            if (ModelState.IsValid)
            {
                tourChiPhiChiTiet.DoanId = (int)doanId;
                context.Add(tourChiPhiChiTiet);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = tourChiPhiChiTiet.DoanId });
            }
            var doan = context.TourDoan.Find(tourChiPhiChiTiet.DoanId);

            if (doan == null)
            {
                return NotFound();
            }

            ViewData["DoanId"] = doan.DoanTen; 
            ViewData["LoaiChiPhiId"] = new SelectList(context.TourLoaiChiPhi, "LoaiChiPhiId", "LoaiChiPhiTen", tourChiPhiChiTiet.LoaiChiPhiId);
            return View(tourChiPhiChiTiet);
        }

        public async Task<IActionResult> EditChiPhiChiTiet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiPhiChiTiet = await context.TourChiPhiChiTiet.Include(tcpct => tcpct.Doan).Include(tcpct => tcpct.LoaiChiPhi).FirstOrDefaultAsync(tcpct => tcpct.ChiPhiChiTietId == id);
            
            if (chiPhiChiTiet == null)
            {
                return NotFound();
            }
            
            ViewData["DoanId"] = chiPhiChiTiet.Doan.DoanTen;
            ViewData["LoaiChiPhiId"] = chiPhiChiTiet.LoaiChiPhi.LoaiChiPhiTen;
            return View(chiPhiChiTiet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChiPhiChiTiet(int id, [Bind("ChiPhiChiTietId,ChiPhi,NgayTao")] TourChiPhiChiTiet tourChiPhiChiTiet)
        {
            if (id != tourChiPhiChiTiet.ChiPhiChiTietId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cpct = await context.TourChiPhiChiTiet.FindAsync(id);
                    cpct.ChiPhi = tourChiPhiChiTiet.ChiPhi;
                    context.Update(cpct);
                    await context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Details), new { id = doanId });
            }
            var chiPhiChiTiet = await context.TourChiPhiChiTiet.Include(tcpct => tcpct.Doan).Include(tcpct => tcpct.LoaiChiPhi).FirstOrDefaultAsync(tcpct => tcpct.ChiPhiChiTietId == id);

            if (chiPhiChiTiet == null)
            {
                return NotFound();
            }

            ViewData["DoanId"] = chiPhiChiTiet.Doan.DoanTen;
            ViewData["LoaiChiPhiId"] = chiPhiChiTiet.LoaiChiPhi.LoaiChiPhiTen; 
            return View(tourChiPhiChiTiet);
        }

        private bool TourChiPhiChiTietExists(int id)
        {
            return context.TourChiPhiChiTiet.Any(e => e.ChiPhiChiTietId == id);
        }

        public async Task<IActionResult> DetailsChiPhiChiTiet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhiChiTiet = await context.TourChiPhiChiTiet
                .Include(t => t.Doan)
                .Include(t => t.LoaiChiPhi)
                .FirstOrDefaultAsync(m => m.ChiPhiChiTietId == id);
            if (tourChiPhiChiTiet == null)
            {
                return NotFound();
            }

            ViewData["DoanId"] = doanId;
            return View(tourChiPhiChiTiet);
        }

        public async Task<IActionResult> DeleteChiPhiChiTiet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourChiPhiChiTiet = await context.TourChiPhiChiTiet
                .Include(t => t.Doan)
                .Include(t => t.LoaiChiPhi)
                .FirstOrDefaultAsync(m => m.ChiPhiChiTietId == id);
            if (tourChiPhiChiTiet == null)
            {
                return NotFound();
            }

            return View(tourChiPhiChiTiet);
        }

        [HttpPost, ActionName("DeleteChiPhiChiTiet")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteChiPhiChiTietConfirmed(int id)
        {
            var tourChiPhiChiTiet = await context.TourChiPhiChiTiet.FindAsync(id);
            context.TourChiPhiChiTiet.Remove(tourChiPhiChiTiet);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = tourChiPhiChiTiet.DoanId });
        }
    }
}
