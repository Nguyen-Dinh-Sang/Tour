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
        private readonly TourDBContext _context;

        public DoanKhachHangsController()
        {
            _context = new TourDBContext();
        }

        // GET: DoanKhachHangs
        public async Task<IActionResult> Index()
        {
            var tourDBContext = _context.DoanKhachHang.Include(d => d.Doan).Include(d => d.KhachHang);
            return View(await tourDBContext.ToListAsync());
        }

        // GET: DoanKhachHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanKhachHang = await _context.DoanKhachHang
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
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet");
            ViewData["KhachHangId"] = new SelectList(_context.TourKhachHang, "KhachHangId", "KhachHangChungMinhNhanDan");
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
                _context.Add(doanKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", doanKhachHang.DoanId);
            ViewData["KhachHangId"] = new SelectList(_context.TourKhachHang, "KhachHangId", "KhachHangChungMinhNhanDan", doanKhachHang.KhachHangId);
            return View(doanKhachHang);
        }

        // GET: DoanKhachHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanKhachHang = await _context.DoanKhachHang.FindAsync(id);
            if (doanKhachHang == null)
            {
                return NotFound();
            }
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", doanKhachHang.DoanId);
            ViewData["KhachHangId"] = new SelectList(_context.TourKhachHang, "KhachHangId", "KhachHangChungMinhNhanDan", doanKhachHang.KhachHangId);
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
                    _context.Update(doanKhachHang);
                    await _context.SaveChangesAsync();
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
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", doanKhachHang.DoanId);
            ViewData["KhachHangId"] = new SelectList(_context.TourKhachHang, "KhachHangId", "KhachHangChungMinhNhanDan", doanKhachHang.KhachHangId);
            return View(doanKhachHang);
        }

        // GET: DoanKhachHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanKhachHang = await _context.DoanKhachHang
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
            var doanKhachHang = await _context.DoanKhachHang.FindAsync(id);
            _context.DoanKhachHang.Remove(doanKhachHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoanKhachHangExists(int id)
        {
            return _context.DoanKhachHang.Any(e => e.DoanKhachHangId == id);
        }
    }
}
