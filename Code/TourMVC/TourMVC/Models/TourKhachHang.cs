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
        [Required(ErrorMessage = "Họ Và Tên Không Được Để Trống")]
        public string KhachHangTen { get; set; }
        [Display(Name = "Số Điện Thoại")]
        [Required(ErrorMessage = "Số Điện Thoại Không Được Để Trống")]
        public string KhachHangSoDienThoai { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Không Được Để Trống")]
        public string KhachHangEmail { get; set; }
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày Sinh Không Được Để Trống")]
        public DateTime KhachHangNgaySinh { get; set; }
        [Display(Name = "Số Chứng Minh Nhân Dân")]
        [Required(ErrorMessage = "Số Chứng Minh Nhân Dân Không Được Để Trống")]
        public string KhachHangChungMinhNhanDan { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<DoanKhachHang> DoanKhachHang { get; set; }
    }
}
