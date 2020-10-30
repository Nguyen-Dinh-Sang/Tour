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
        }

        public int TourId { get; set; }
        public string TourTen { get; set; }
        public string TourMoTa { get; set; }
        public int LoaiId { get; set; }
        public int GiaId { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual TourGia Gia { get; set; }
        public virtual TourLoai Loai { get; set; }
        public virtual ICollection<TourChiTiet> TourChiTiet { get; set; }
        public virtual ICollection<TourDoan> TourDoan { get; set; }
    }
}
