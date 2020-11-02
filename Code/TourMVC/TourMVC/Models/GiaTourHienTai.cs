using System;
using System.Collections.Generic;

namespace TourMVC.Models
{
    public partial class GiaTourHienTai
    {
        public int TourId { get; set; }
        public int GiaId { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual TourGia Gia { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
