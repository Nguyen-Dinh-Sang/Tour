using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class GiaTourHienTai
    {
        [Display(Name = "Tour")]
        public int TourId { get; set; }
        [Display(Name = "Giá Từ Ngày")]
        public int GiaId { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Giá Hiện Tại")]
        public virtual TourGia Gia { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
