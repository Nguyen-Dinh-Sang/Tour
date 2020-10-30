using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class DoanKhachHang
    {
        public int DoanKhachHangId { get; set; }
        public int KhachHangId { get; set; }
        public int DoanId { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual TourDoan Doan { get; set; }
        public virtual TourKhachHang KhachHang { get; set; }
    }
}
