using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourChiTiet
    {
        public int ChiTietId { get; set; }
        public int TourId { get; set; }
        public int DiaDiemId { get; set; }
        public int ChiTietThuTu { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual TourDiaDiem DiaDiem { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
