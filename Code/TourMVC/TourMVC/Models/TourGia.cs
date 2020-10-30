using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourGia
    {
        public TourGia()
        {
            Tour = new HashSet<Tour>();
        }

        public int GiaId { get; set; }
        public decimal GiaSoTien { get; set; }
        public int TourId { get; set; }
        public DateTime GiaTuNgay { get; set; }
        public DateTime GiaDenNgay { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<Tour> Tour { get; set; }
    }
}
