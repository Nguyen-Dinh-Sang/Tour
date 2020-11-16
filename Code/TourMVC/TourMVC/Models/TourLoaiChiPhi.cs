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
        [Required(ErrorMessage = "Loại Chi Phí Không Được Để Trống")]
        public string LoaiChiPhiTen { get; set; }
        [Display(Name = "Mô Tả")]
        [Required(ErrorMessage = "Mô Tả Không Được Để Trống")]
        public string LoaiChiPhiMoTa { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<TourChiPhiChiTiet> TourChiPhiChiTiet { get; set; }
    }
}
