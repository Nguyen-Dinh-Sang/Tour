using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourMVC.Models;

namespace TourMVC.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly TourDBContext context;

        public ThongKeController()
        {
            context = new TourDBContext();
        }

        public IActionResult Index()
        {

            var queryTour_Doan = (
                from t in context.Tour
                join td in context.TourDoan on t.TourId equals td.TourId into t_td_join
                from t_td in t_td_join.DefaultIfEmpty()
                select new
                {
                    TourId = t.TourId,
                    TourTen = t.TourTen,
                    DoanId = t_td.DoanId,
                    NgayTao = t_td.NgayTao,
                    Gia = t_td.DoanGiaTour
                }
                );

            // lọc khúc này là ổn rồi

            var queryDoanKhachHang = (
                    from dkh in context.DoanKhachHang
                    group new { dkh } by new
                    {
                        dkh.DoanId
                    } into g
                    select new
                    {
                        DoanId = g.Key.DoanId,
                        SoLuongKhach = g.Count()
                    }
                );

            var queryChiPhi = (
                    from cp in context.TourChiPhiChiTiet
                    group new { cp } by new
                    {
                        cp.DoanId
                    } into g
                    select new
                    {
                        DoanId = g.Key.DoanId,
                        TongChiPhi = (decimal?)g.Sum(cp => cp.cp.ChiPhi)
                    }
                );

            var queryDoan_KhachHang = (
                    from td in queryTour_Doan
                    join kh in queryDoanKhachHang on td.DoanId equals kh.DoanId into td_kh_join
                    from td_kh in td_kh_join.DefaultIfEmpty()
                    select new
                    {

                        TourId = td.TourId,
                        TourTen = td.TourTen,
                        DoanId = td.DoanId,
                        Gia = td.Gia,
                        SoLuongKhach = td_kh.SoLuongKhach
                    }
                );

            var queryDoan_KhachHang_ChiPhi = (
                    from tdkh in queryDoan_KhachHang
                    join cp in queryChiPhi on tdkh.DoanId equals cp.DoanId into tdkh_cp_join
                    from tdkh_cp in tdkh_cp_join.DefaultIfEmpty()
                    select new
                    {
                        TourId = tdkh.TourId,
                        TourTen = tdkh.TourTen,
                        DoanId = tdkh.DoanId,
                        Gia = tdkh.Gia,
                        SoLuongKhach = tdkh.SoLuongKhach,
                        TongChiPhi = tdkh_cp.TongChiPhi
                    }
                );

            var queryDoan_KhachHang_ChiPhi_DoanhThu = (
                from dkhcp in queryDoan_KhachHang_ChiPhi
                select new
                {
                    TourId = dkhcp.TourId,
                    TourTen = dkhcp.TourTen,
                    DoanId = dkhcp.DoanId,
                    TongDoanhThu = (decimal?)(dkhcp.Gia * dkhcp.SoLuongKhach),
                    TongChiPhi = dkhcp.TongChiPhi
                }
                );

            var queryTour_DoanhThu_ChiPhi = (
                from dkhcpdt in queryDoan_KhachHang_ChiPhi_DoanhThu
                where dkhcpdt.DoanId > 0
                group new { dkhcpdt } by new
                {
                    dkhcpdt.TourId
                } into g
                select new
                {
                    TourId = g.Key.TourId,
                    TongDoanhThu = (decimal?)g.Sum(dkhcpdt => dkhcpdt.dkhcpdt.TongDoanhThu),
                    TongChiPhi = (decimal?)g.Sum(dkhcpdt => dkhcpdt.dkhcpdt.TongChiPhi),
                    TongSoDoan = g.Count()
                }
                );

            var queryCuoiCung = (
                    from t in context.Tour
                    join tdtcp in queryTour_DoanhThu_ChiPhi on t.TourId equals tdtcp.TourId into t_tdtcp_join
                    from t_tdtcp in t_tdtcp_join.DefaultIfEmpty()
                    select new ThongKeTour
                    {
                        TourId = t.TourId,
                        TourTen = t.TourTen,
                        TongSoDoan = t_tdtcp.TongSoDoan == null ? 0 : t_tdtcp.TongSoDoan,
                        TongDoanhThu = t_tdtcp.TongDoanhThu == null ? 0 : t_tdtcp.TongDoanhThu,
                        TongChiPhi = t_tdtcp.TongChiPhi == null ? 0 : t_tdtcp.TongChiPhi
                    }
                );

            var tongDoanhThu = queryCuoiCung.Sum(i => i.TongDoanhThu);
            var tongChiPhi = queryCuoiCung.Sum(i => i.TongChiPhi); 

            ViewData["TongSoDoan"] = queryCuoiCung.Sum(i => i.TongSoDoan);
            ViewData["TongDoanhThu"] = tongDoanhThu;
            ViewData["TongChiPhi"] = tongChiPhi;
            ViewData["TongLoiNhuan"] = (tongDoanhThu - tongChiPhi);

            return View(queryCuoiCung);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await context.Tour.FindAsync(id);
            ViewData["Tour"] = tour.TourTen;

            var queryDoan = (
                from d in context.TourDoan
                where d.TourId == id
                select new
                {
                    DoanId = d.DoanId,
                    DoanTen = d.DoanTen,
                    NgayTao = d.NgayTao,
                    GiaTien = d.DoanGiaTour
                }
                );

            // lọc khúc này là ổn rồi

            var queryDoanKhachHang = (
                    from dkh in context.DoanKhachHang
                    group new { dkh } by new
                    {
                        dkh.DoanId
                    } into g
                    select new
                    {
                        DoanId = g.Key.DoanId,
                        SoLuongKhach = g.Count()
                    }
                );

            var queryChiPhi = (
                    from cp in context.TourChiPhiChiTiet
                    group new { cp } by new
                    {
                        cp.DoanId
                    } into g
                    select new
                    {
                        DoanId = g.Key.DoanId,
                        TongChiPhi = (decimal?)g.Sum(cp => cp.cp.ChiPhi)
                    }
                );

            var queryDoan_KhachHang = (
                    from td in queryDoan
                    join kh in queryDoanKhachHang on td.DoanId equals kh.DoanId into td_kh_join
                    from td_kh in td_kh_join.DefaultIfEmpty()
                    select new
                    {

                        DoanId = td.DoanId,
                        DoanTen = td.DoanTen,
                        GiaTien = td.GiaTien,
                        SoLuongKhach = td_kh.SoLuongKhach
                    }
                );

            var queryCuoiCung = (
                   from tdkh in queryDoan_KhachHang
                   join cp in queryChiPhi on tdkh.DoanId equals cp.DoanId into tdkh_cp_join
                   from tdkh_cp in tdkh_cp_join.DefaultIfEmpty()
                   select new ThongKeDoan
                   {
                       DoanId = tdkh.DoanId,
                       DoanTen = tdkh.DoanTen,
                       SoLuongKhach = tdkh.SoLuongKhach,
                       GiaTien = tdkh.GiaTien,
                       TongDoanhThu = (decimal?)(tdkh.GiaTien * tdkh.SoLuongKhach) == null ? 0 : (decimal?)(tdkh.GiaTien * tdkh.SoLuongKhach),
                       TongChiPhi = tdkh_cp.TongChiPhi == null ? 0 : tdkh_cp.TongChiPhi
                   }
               );

            var tongDoanhThu = queryCuoiCung.Sum(i => i.TongDoanhThu);
            var tongChiPhi = queryCuoiCung.Sum(i => i.TongChiPhi);

            ViewData["TongSoKhach"] = queryCuoiCung.Sum(i => i.SoLuongKhach);
            ViewData["TongDoanhThu"] = tongDoanhThu;
            ViewData["TongChiPhi"] = tongChiPhi;
            ViewData["TongLoiNhuan"] = (tongDoanhThu - tongChiPhi);

            return View(queryCuoiCung);
        }

        public async Task<IActionResult> DetailsDoan(int? id)
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

            return View(tourDoan);
        }
    }
}