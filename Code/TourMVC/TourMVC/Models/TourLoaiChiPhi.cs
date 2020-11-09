using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourLoaiChiPhi
    {
        public TourLoaiChiPhi()
        {
            TourChiPhiChiTiet = new HashSet<TourChiPhiChiTiet>();
        }

        public int LoaiChiPhiId { get; set; }
        public string LoaiChiPhiTen { get; set; }
        public string LoaiChiPhiMoTa { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<TourChiPhiChiTiet> TourChiPhiChiTiet { get; set; }
    }
}
