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
        private readonly TourDBContext _context;

        public TourNhanViensController()
        {
            _context = new TourDBContext();
        }

        // GET: TourNhanViens
        public async Task<IActionResult> Index()
        {
            return View(await _context.TourNhanVien.ToListAsync());
        }

        // GET: TourNhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourNhanVien = await _context.TourNhanVien
                .FirstOrDefaultAsync(m => m.NhanVienId == id);
            if (tourNhanVien == null)
            {
                return NotFound();
            }

            return View(tourNhanVien);
        }

        // GET: TourNhanViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TourNhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhanVienId,NhanVienTen,NhanVienSoDienThoai,NhanVienEmail,NhanVienNgaySinh,NgayTao")] TourNhanVien tourNhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourNhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourNhanVien);
        }

        // GET: TourNhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourNhanVien = await _context.TourNhanVien.FindAsync(id);
            if (tourNhanVien == null)
            {
                return NotFound();
            }
            return View(tourNhanVien);
        }

        // POST: TourNhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(tourNhanVien);
                    await _context.SaveChangesAsync();
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

        // GET: TourNhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourNhanVien = await _context.TourNhanVien
                .FirstOrDefaultAsync(m => m.NhanVienId == id);
            if (tourNhanVien == null)
            {
                return NotFound();
            }

            return View(tourNhanVien);
        }

        // POST: TourNhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourNhanVien = await _context.TourNhanVien.FindAsync(id);
            _context.TourNhanVien.Remove(tourNhanVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourNhanVienExists(int id)
        {
            return _context.TourNhanVien.Any(e => e.NhanVienId == id);
        }
    }
}
