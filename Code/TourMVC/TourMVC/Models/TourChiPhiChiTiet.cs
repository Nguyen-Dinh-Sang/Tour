using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourChiPhiChiTiet
    {
        public int ChiPhiChiTietId { get; set; }
        public int DoanId { get; set; }
        public int LoaiChiPhiId { get; set; }
        public decimal ChiPhi { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual TourDoan Doan { get; set; }
        public virtual TourLoaiChiPhi LoaiChiPhi { get; set; }
    }
}
