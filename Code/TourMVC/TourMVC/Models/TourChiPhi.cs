using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourChiPhi
    {
        public TourChiPhi()
        {
            TourChiPhiChiTiet = new HashSet<TourChiPhiChiTiet>();
        }

        public int ChiPhiId { get; set; }
        public int DoanId { get; set; }
        public decimal ChiPhiTong { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual TourDoan Doan { get; set; }
        public virtual ICollection<TourChiPhiChiTiet> TourChiPhiChiTiet { get; set; }
    }
}
