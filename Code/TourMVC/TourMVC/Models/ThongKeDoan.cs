using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourMVC.Models
{
    public partial class ThongKeDoan
    {
        public ThongKeDoan()
        {
        }

        public int DoanId { get; set; }
        [Display(Name = "Đoàn")]
        public string DoanTen { get; set; }

        [Display(Name = "Số Lượng Khách")]
        public int SoLuongKhach { get; set; }

        [Display(Name = "Giá Tour")]
        public decimal? GiaTien { get; set; }

        [Display(Name = "Tổng Doanh Thu")]
        public decimal? TongDoanhThu { get; set; }

        [Display(Name = "Tổng Chi Phí")]
        public decimal? TongChiPhi { get; set; }
    }
}
