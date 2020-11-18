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
            var query = (from t in context.Tour
                         join d in (
                             (from td in context.TourDoan
                              join dkh in context.DoanKhachHang on td.DoanId equals dkh.DoanId into dkh_join
                              from dkh in dkh_join.DefaultIfEmpty()
                              join cpct in context.TourChiPhiChiTiet on td.DoanId equals cpct.DoanId into cpct_join
                              from cpct in cpct_join.DefaultIfEmpty()
                              group new { td, dkh, cpct } by new
                              {
                                  td.TourId,
                                  td.DoanId,
                                  td.DoanGiaTour
                              } into g
                              select new
                              {
                                  g.Key.TourId,
                                  Doan_ID = (int?)g.Key.DoanId,
                                  SoLuong_Khach = g.Count(p => p.dkh.DoanKhachHangId != null),
                                  DoanhThu = (decimal?)(g.Count(p => p.dkh.DoanKhachHangId != null) * g.Key.DoanGiaTour),
                                  ChiPhi = (decimal?)g.Sum(p => p.cpct.ChiPhi)
                              })) on new { Tour_ID = t.TourId } equals new { Tour_ID = Convert.ToInt32(d.TourId) } into d_join
                         from d in d_join.DefaultIfEmpty()
                         group new { t, d } by new
                         {
                             t.TourId,
                             t.TourTen
                         } into g
                         select new
                         {
                             g.Key.TourId,
                             g.Key.TourTen,
                             Tong_SoDoan = g.Count(p => p.d.Doan_ID != null),
                             Tong_DoanhThu = (decimal?)g.Sum(p => p.d.DoanhThu),
                             Tong_ChiPhi = (decimal?)g.Sum(p => p.d.ChiPhi)
                         }).AsEnumerable();


            

            foreach (var item in query)
            {
                Console.WriteLine($"{item.TourId + ":",-15}{item.TourTen + ":",-15}");
            }
            return View();
        }

        private object coalesce(decimal? v1, int v2)
        {
            if (v1 == null)
            {
                return v2;
            }

            return v1;
        }
    }
}