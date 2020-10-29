using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entitys
{
    public partial class WorkListStatus
    {
        public WorkListStatus()
        {
            WorkList = new HashSet<WorkList>();
        }

        [Key]
        public int Id { get; set; }
        [Column("WorkListStatus")]
        [StringLength(100)]
        public string WorkListStatus1 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [InverseProperty("IdWorkListStatusNavigation")]
        public virtual ICollection<WorkList> WorkList { get; set; }
    }
}
