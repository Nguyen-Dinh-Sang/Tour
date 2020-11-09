using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourLoaiChiPhi
    {
        public TourLoaiChiPhi()
        {
            TourChiPhiChiTiet = new HashSet<TourChiPhiChiTiet>();
        }

        public int LoaiChiPhiId { get; set; }
        [Display(Name = "Loại Chi Phí")]
        public string LoaiChiPhiTen { get; set; }
        [Display(Name = "Mô Tả")]
        public string LoaiChiPhiMoTa { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<TourChiPhiChiTiet> TourChiPhiChiTiet { get; set; }
    }
}
