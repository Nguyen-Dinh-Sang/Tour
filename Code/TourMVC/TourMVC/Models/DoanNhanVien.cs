using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class DoanNhanVien
    {
        public int DoanNhanVienId { get; set; }
        public int NhanVienId { get; set; }
        public int DoanId { get; set; }
        public string NhanVienNhiemVu { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual TourDoan Doan { get; set; }
        public virtual TourNhanVien NhanVien { get; set; }
    }
}
