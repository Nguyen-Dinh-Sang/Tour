using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class DoanNhanVien
    {
        public int DoanNhanVienId { get; set; }
        [Display(Name = "Nhân Viên")]
        public int NhanVienId { get; set; }
        [Display(Name = "Đoàn")]
        public int DoanId { get; set; }
        [Display(Name = "Nhiệm Vụ")]
        public string NhanVienNhiemVu { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Đoàn")]
        public virtual TourDoan Doan { get; set; }
        [Display(Name = "Nhân Viên")]
        public virtual TourNhanVien NhanVien { get; set; }
    }
}
