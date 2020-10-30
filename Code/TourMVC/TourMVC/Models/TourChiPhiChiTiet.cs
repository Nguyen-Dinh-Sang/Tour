using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourChiPhiChiTiet
    {
        public int ChiPhiChiTietId { get; set; }
        public int ChiPhiId { get; set; }
        public int LoaiChiPhiId { get; set; }
        public decimal ChiPhi { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual TourChiPhi ChiPhiNavigation { get; set; }
        public virtual TourLoaiChiPhi LoaiChiPhi { get; set; }
    }
}
