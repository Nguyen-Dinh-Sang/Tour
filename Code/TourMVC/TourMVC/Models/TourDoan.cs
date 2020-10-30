using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourDoan
    {
        public TourDoan()
        {
            DoanKhachHang = new HashSet<DoanKhachHang>();
            DoanNhanVien = new HashSet<DoanNhanVien>();
            TourChiPhi = new HashSet<TourChiPhi>();
        }

        public int DoanId { get; set; }
        public int TourId { get; set; }
        public string DoanTen { get; set; }
        public DateTime DoanNgayDi { get; set; }
        public DateTime DoanNgayVe { get; set; }
        public string DoanChiTiet { get; set; }
        public decimal DoanGiaTour { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual ICollection<DoanKhachHang> DoanKhachHang { get; set; }
        public virtual ICollection<DoanNhanVien> DoanNhanVien { get; set; }
        public virtual ICollection<TourChiPhi> TourChiPhi { get; set; }
    }
}
