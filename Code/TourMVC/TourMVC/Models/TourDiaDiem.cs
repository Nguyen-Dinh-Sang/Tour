using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourDiaDiem
    {
        public TourDiaDiem()
        {
            TourChiTiet = new HashSet<TourChiTiet>();
        }

        public int DiaDiemId { get; set; }
        [Display(Name = "Thành Phố")]
        public string DiaDiemThanhPho { get; set; }
        [Display(Name = "Tên Địa Điểm")]
        public string DiaDiemTen { get; set; }
        [Display(Name = "Mô Tả")]
        public string DiaDiemMoTa { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<TourChiTiet> TourChiTiet { get; set; }
    }
}
