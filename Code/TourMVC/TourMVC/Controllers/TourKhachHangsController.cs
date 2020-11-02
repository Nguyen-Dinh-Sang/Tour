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
        private readonly TourDBContext _context;

        public TourKhachHangsController()
        {
            _context = new TourDBContext();
        }

        // GET: TourKhachHangs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TourKhachHang.ToListAsync());
        }

        // GET: TourKhachHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourKhachHang = await _context.TourKhachHang
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KhachHangId,KhachHangTen,KhachHangSoDienThoai,KhachHangEmail,KhachHangNgaySinh,KhachHangChungMinhNhanDan,NgayTao")] TourKhachHang tourKhachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourKhachHang);
                await _context.SaveChangesAsync();
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

            var tourKhachHang = await _context.TourKhachHang.FindAsync(id);
            if (tourKhachHang == null)
            {
                return NotFound();
            }
            return View(tourKhachHang);
        }

        // POST: TourKhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(tourKhachHang);
                    await _context.SaveChangesAsync();
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

            var tourKhachHang = await _context.TourKhachHang
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
            var tourKhachHang = await _context.TourKhachHang.FindAsync(id);
            _context.TourKhachHang.Remove(tourKhachHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourKhachHangExists(int id)
        {
            return _context.TourKhachHang.Any(e => e.KhachHangId == id);
        }
    }
}
