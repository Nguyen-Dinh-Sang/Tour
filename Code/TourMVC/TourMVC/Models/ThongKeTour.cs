using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourMVC.Models
{
    public partial class ThongKeTour
    {
        public ThongKeTour()
        {
        }

        public int TourId { get; set; }
        [Display(Name = "Tour")]
        public string TourTen { get; set; }

        [Display(Name = "Tổng Số Đoàn Đi")]
        public int TongSoDoan { get; set; }

        [Display(Name = "Tổng Doanh Thu")]
        public decimal TongDoanhThu { get; set; }

        [Display(Name = "Tổng Chi Phí")]
        public decimal TongChiPhi { get; set; }
    }
}
