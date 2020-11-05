﻿using System;
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
        public decimal GiaSoTien { get; set; }
        public int TourId { get; set; }
        [Display(Name = "Giá Từ Ngày")]
        [DataType(DataType.Date)]
        public DateTime GiaTuNgay { get; set; }
        [Display(Name = "Giá Đến Ngày")]
        [DataType(DataType.Date)]
        public DateTime GiaDenNgay { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual ICollection<GiaTourHienTai> GiaTourHienTai { get; set; }
    }
}
