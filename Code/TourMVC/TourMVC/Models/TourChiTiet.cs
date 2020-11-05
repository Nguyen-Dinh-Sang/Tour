using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourChiTiet
    {
        public int ChiTietId { get; set; }
        [Display(Name = "Tour")]
        public int TourId { get; set; }
        [Display(Name = "Địa Điểm")]
        public int DiaDiemId { get; set; }
        [Display(Name = "Thứ Tự")]
        public int ChiTietThuTu { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Địa Điểm")]
        public virtual TourDiaDiem DiaDiem { get; set; }
        [Display(Name = "Tour")]
        public virtual Tour Tour { get; set; }
    }
}
