using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourNhanVien
    {
        public TourNhanVien()
        {
            DoanNhanVien = new HashSet<DoanNhanVien>();
        }

        public int NhanVienId { get; set; }
        [Display(Name = "Họ Và Tên")]
        [Required(ErrorMessage = "Họ Và Tên Không Được Để Trống")]
        public string NhanVienTen { get; set; }
        [Display(Name = "Số Điện Thoại")]
        [Required(ErrorMessage = "Số Điện Thoại Không Được Để Trống")]
        public string NhanVienSoDienThoai { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Không Được Để Trống")]
        public string NhanVienEmail { get; set; }
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày Sinh Không Được Để Trống")]
        public DateTime NhanVienNgaySinh { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<DoanNhanVien> DoanNhanVien { get; set; }
    }
}
