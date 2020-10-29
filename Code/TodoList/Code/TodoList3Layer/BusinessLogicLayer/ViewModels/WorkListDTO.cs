using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.ViewModels
{
    public class WorkListDTO
    {
        public int Id { get; set; }
        public string WorkListName { get; set; }
        public int? IdWorkListStatus { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
