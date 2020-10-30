using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class TourLoai
    {
        public TourLoai()
        {
            Tour = new HashSet<Tour>();
        }

        public int LoaiId { get; set; }
        public string LoaiTen { get; set; }
        public string LoaiMoTa { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<Tour> Tour { get; set; }
    }
}
