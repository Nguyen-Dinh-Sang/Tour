using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourDiaDiem
    {
        public TourDiaDiem()
        {
            TourChiTiet = new HashSet<TourChiTiet>();
        }

        public int DiaDiemId { get; set; }
        public string DiaDiemThanhPho { get; set; }
        public string DiaDiemTen { get; set; }
        public string DiaDiemMoTa { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<TourChiTiet> TourChiTiet { get; set; }
    }
}
