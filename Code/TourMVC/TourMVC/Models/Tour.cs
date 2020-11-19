using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class Tour
    {
        public Tour()
        {
            TourChiTiet = new HashSet<TourChiTiet>();
            TourDoan = new HashSet<TourDoan>();
            TourGia = new HashSet<TourGia>();
        }

        public int TourId { get; set; }
        [Display(Name = "Tên Tour")]
        [Required(ErrorMessage = "Tour Không Được Để Trống")]
        public string TourTen { get; set; }
        [Display(Name = "Mô Tả")]
        [Required(ErrorMessage = "Mô Tả Không Được Để Trống")]
        public string TourMoTa { get; set; }
        [Display(Name = "Loại Tour")]
        public int LoaiId { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Loại Tour")]
        public virtual TourLoai Loai { get; set; }
        [Display(Name = "Giá Hiện Tại")]
        public virtual GiaTourHienTai GiaTourHienTai { get; set; }
        public virtual ICollection<TourChiTiet> TourChiTiet { get; set; }
        public virtual ICollection<TourDoan> TourDoan { get; set; }
        public virtual ICollection<TourGia> TourGia { get; set; }
    }
}
