using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourGia
    {
        public TourGia()
        {
            GiaTourHienTai = new HashSet<GiaTourHienTai>();
        }

        public int GiaId { get; set; }
        [Display(Name = "Số Tiền")]
        [Required(ErrorMessage = "Giá Không Được Để Trống")]
        public decimal GiaSoTien { get; set; }
        [Display(Name = "Tour")]
        public int TourId { get; set; }
        [Display(Name = "Giá Từ Ngày")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày Bắt Đầu Không Được Để Trống")]
        public DateTime GiaTuNgay { get; set; }
        [Display(Name = "Giá Đến Ngày")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày Kết Thúc Không Được Để Trống")]
        public DateTime GiaDenNgay { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual ICollection<GiaTourHienTai> GiaTourHienTai { get; set; }
    }
}
