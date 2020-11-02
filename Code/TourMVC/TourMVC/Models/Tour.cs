using System;
using System.Collections.Generic;

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
        public string TourTen { get; set; }
        public string TourMoTa { get; set; }
        public int LoaiId { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual TourLoai Loai { get; set; }
        public virtual GiaTourHienTai GiaTourHienTai { get; set; }
        public virtual ICollection<TourChiTiet> TourChiTiet { get; set; }
        public virtual ICollection<TourDoan> TourDoan { get; set; }
        public virtual ICollection<TourGia> TourGia { get; set; }
    }
}
