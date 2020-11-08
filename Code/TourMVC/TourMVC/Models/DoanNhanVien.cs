using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class DoanNhanVien
    {
        public int DoanNhanVienId { get; set; }
        public int NhanVienId { get; set; }
        public int DoanId { get; set; }
        public string NhanVienNhiemVu { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual TourDoan Doan { get; set; }
        public virtual TourNhanVien NhanVien { get; set; }
    }
}
