using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourKhachHang
    {
        public TourKhachHang()
        {
            DoanKhachHang = new HashSet<DoanKhachHang>();
        }

        public int KhachHangId { get; set; }
        public string KhachHangTen { get; set; }
        public string KhachHangSoDienThoai { get; set; }
        public string KhachHangEmail { get; set; }
        public DateTime KhachHangNgaySinh { get; set; }
        public string KhachHangChungMinhNhanDan { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<DoanKhachHang> DoanKhachHang { get; set; }
    }
}
