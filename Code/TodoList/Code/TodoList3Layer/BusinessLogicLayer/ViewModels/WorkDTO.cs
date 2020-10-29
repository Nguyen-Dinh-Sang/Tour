using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogicLayer.ViewModels
{
    public partial class WorkDTO
    {
        public int Id { get; set; }
        public int? IdWorkList { get; set; }
        public int? IdWorkStatus { get; set; }
        public string WorkContent { get; set; }
        public string WorkName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
