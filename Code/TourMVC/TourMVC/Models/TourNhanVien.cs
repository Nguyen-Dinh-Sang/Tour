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
        public string NhanVienTen { get; set; }
        [Display(Name = "Số Điện Thoại")]
        public string NhanVienSoDienThoai { get; set; }
        [Display(Name = "Email")]
        public string NhanVienEmail { get; set; }
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateTime NhanVienNgaySinh { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<DoanNhanVien> DoanNhanVien { get; set; }
    }
}
