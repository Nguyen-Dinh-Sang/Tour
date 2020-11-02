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
    public class DoanNhanViensController : Controller
    {
        private readonly TourDBContext _context;

        public DoanNhanViensController()
        {
            _context = new TourDBContext();
        }

        // GET: DoanNhanViens
        public async Task<IActionResult> Index()
        {
            var tourDBContext = _context.DoanNhanVien.Include(d => d.Doan).Include(d => d.NhanVien);
            return View(await tourDBContext.ToListAsync());
        }

        // GET: DoanNhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanNhanVien = await _context.DoanNhanVien
                .Include(d => d.Doan)
                .Include(d => d.NhanVien)
                .FirstOrDefaultAsync(m => m.DoanNhanVienId == id);
            if (doanNhanVien == null)
            {
                return NotFound();
            }

            return View(doanNhanVien);
        }

        // GET: DoanNhanViens/Create
        public IActionResult Create()
        {
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet");
            ViewData["NhanVienId"] = new SelectList(_context.TourNhanVien, "NhanVienId", "NhanVienEmail");
            return View();
        }

        // POST: DoanNhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoanNhanVienId,NhanVienId,DoanId,NhanVienNhiemVu,NgayTao")] DoanNhanVien doanNhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doanNhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", doanNhanVien.DoanId);
            ViewData["NhanVienId"] = new SelectList(_context.TourNhanVien, "NhanVienId", "NhanVienEmail", doanNhanVien.NhanVienId);
            return View(doanNhanVien);
        }

        // GET: DoanNhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanNhanVien = await _context.DoanNhanVien.FindAsync(id);
            if (doanNhanVien == null)
            {
                return NotFound();
            }
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", doanNhanVien.DoanId);
            ViewData["NhanVienId"] = new SelectList(_context.TourNhanVien, "NhanVienId", "NhanVienEmail", doanNhanVien.NhanVienId);
            return View(doanNhanVien);
        }

        // POST: DoanNhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoanNhanVienId,NhanVienId,DoanId,NhanVienNhiemVu,NgayTao")] DoanNhanVien doanNhanVien)
        {
            if (id != doanNhanVien.DoanNhanVienId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doanNhanVien);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoanId"] = new SelectList(_context.TourDoan, "DoanId", "DoanChiTiet", doanNhanVien.DoanId);
            ViewData["NhanVienId"] = new SelectList(_context.TourNhanVien, "NhanVienId", "NhanVienEmail", doanNhanVien.NhanVienId);
            return View(doanNhanVien);
        }

        // GET: DoanNhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doanNhanVien = await _context.DoanNhanVien
                .Include(d => d.Doan)
                .Include(d => d.NhanVien)
                .FirstOrDefaultAsync(m => m.DoanNhanVienId == id);
            if (doanNhanVien == null)
            {
                return NotFound();
            }

            return View(doanNhanVien);
        }

        // POST: DoanNhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doanNhanVien = await _context.DoanNhanVien.FindAsync(id);
            _context.DoanNhanVien.Remove(doanNhanVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoanNhanVienExists(int id)
        {
            return _context.DoanNhanVien.Any(e => e.DoanNhanVienId == id);
        }
    }
}
