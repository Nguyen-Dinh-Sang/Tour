using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class DoanKhachHang
    {
        public int DoanKhachHangId { get; set; }
        [Display(Name = "Khách Hàng")]
        public int KhachHangId { get; set; }
        [Display(Name = "Đoàn Du Lịch")]
        public int DoanId { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Đoàn")]
        public virtual TourDoan Doan { get; set; }
        [Display(Name = "Khách Hàng")]
        public virtual TourKhachHang KhachHang { get; set; }
    }
}
