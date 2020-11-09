using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourChiPhiChiTiet
    {
        public int ChiPhiChiTietId { get; set; }
        [Display(Name = "Đoàn")]
        public int DoanId { get; set; }
        [Display(Name = "Loại Chi Phí")]
        public int LoaiChiPhiId { get; set; }
        [Display(Name = "Chi Phí")]
        public decimal ChiPhi { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Đoàn")]
        public virtual TourDoan Doan { get; set; }
        [Display(Name = "Chi Tiết Chi Phí")]
        public virtual TourLoaiChiPhi LoaiChiPhi { get; set; }
    }
}
