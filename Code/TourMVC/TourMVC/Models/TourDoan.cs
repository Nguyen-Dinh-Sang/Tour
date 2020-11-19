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
        [Required(ErrorMessage = "Đoàn Không Được Để Trống")]
        public string DoanTen { get; set; }
        [Display(Name = "Ngày Đi")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày Đi Không Được Để Trống")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DoanNgayDi { get; set; }
        [Display(Name = "Ngày Về")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày Về Không Được Để Trống")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DoanNgayVe { get; set; }
        [Display(Name = "Chi Tiết Đoàn")]
        [Required(ErrorMessage = "Chi Tiết Không Được Để Trống")]
        public string DoanChiTiet { get; set; }
        [Display(Name = "Giá Tour")]
        public decimal DoanGiaTour { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayTao { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual ICollection<DoanKhachHang> DoanKhachHang { get; set; }
        public virtual ICollection<DoanNhanVien> DoanNhanVien { get; set; }
        public virtual ICollection<TourChiPhiChiTiet> TourChiPhiChiTiet { get; set; }
    }
}
