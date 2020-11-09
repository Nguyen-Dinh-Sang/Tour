using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourDoan
    {
        public TourDoan()
        {
            DoanKhachHang = new HashSet<DoanKhachHang>();
            DoanNhanVien = new HashSet<DoanNhanVien>();
            TourChiPhiChiTiet = new HashSet<TourChiPhiChiTiet>();
        }

        public int DoanId { get; set; }
        [Display(Name = "Tour")]
        public int TourId { get; set; }
        [Display(Name = "Tên Đoàn")]
        public string DoanTen { get; set; }
        [Display(Name = "Ngày Đi")]
        [DataType(DataType.Date)]
        public DateTime DoanNgayDi { get; set; }
        [Display(Name = "Ngày Về")]
        [DataType(DataType.Date)]
        public DateTime DoanNgayVe { get; set; }
        [Display(Name = "Chi Tiết Đoàn")]
        public string DoanChiTiet { get; set; }
        [Display(Name = "Giá Tour")]
        public decimal DoanGiaTour { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual ICollection<DoanKhachHang> DoanKhachHang { get; set; }
        public virtual ICollection<DoanNhanVien> DoanNhanVien { get; set; }
        public virtual ICollection<TourChiPhiChiTiet> TourChiPhiChiTiet { get; set; }
    }
}
