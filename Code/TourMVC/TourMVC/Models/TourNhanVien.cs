using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourNhanVien
    {
        public TourNhanVien()
        {
            DoanNhanVien = new HashSet<DoanNhanVien>();
        }

        public int NhanVienId { get; set; }
        public string NhanVienTen { get; set; }
        public string NhanVienSoDienThoai { get; set; }
        public string NhanVienEmail { get; set; }
        public DateTime NhanVienNgaySinh { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<DoanNhanVien> DoanNhanVien { get; set; }
    }
}
