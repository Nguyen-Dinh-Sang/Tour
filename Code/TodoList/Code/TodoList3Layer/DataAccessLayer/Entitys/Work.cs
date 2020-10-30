using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entitys
{
    public partial class Work
    {
        public Work()
        {
            Comment = new HashSet<Comment>();
            WorkEmployee = new HashSet<WorkEmployee>();
        }

        public int Id { get; set; }
        public int? IdWorkList { get; set; }
        public int? IdWorkStatus { get; set; }
        public string WorkContent { get; set; }
        public string WorkName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual WorkList IdWorkListNavigation { get; set; }
        public virtual WorkStatus IdWorkStatusNavigation { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<WorkEmployee> WorkEmployee { get; set; }
    }
}
