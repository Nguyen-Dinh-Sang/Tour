﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourMVC.Models
{
    public partial class TourLoai
    {
        public TourLoai()
        {
            Tour = new HashSet<Tour>();
        }

        public int LoaiId { get; set; }
        [Display(Name = "Tên Loại")]
        [Required(ErrorMessage = "Tên Loại Không Được Để Trống")]
        public string LoaiTen { get; set; }
        [Display(Name = "Mô Tả")]
        [Required(ErrorMessage = "Mô Tả Không Được Để Trống")]
        public string LoaiMoTa { get; set; }
        [Display(Name = "Ngày Tạo")]
        [DataType(DataType.Date)]
        public DateTime? NgayTao { get; set; }

        public virtual ICollection<Tour> Tour { get; set; }
    }
}
