using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourKhachHang
    {
        public TourKhachHang()
        {
            DoanKhachHang = new HashSet<DoanKhachHang>();
        }

        public int KhachHangId { get; set; }
        [Display(Name = "Họ Và Tên")]
        public string KhachHangTen { get; set; }
        [Display(Name = "Số Điện Thoại")]
        public string KhachHangSoDienThoai { get; set; }
        [Display(Name = "Email")]
        public string KhachHangEmail { get; set; }
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateTime KhachHangNgaySinh { get; set; }
        [Display(Name = "Số Chứng Minh Nhân Dân")]
        public string KhachHangChungMinhNhanDan { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<DoanKhachHang> DoanKhachHang { get; set; }
    }
}
