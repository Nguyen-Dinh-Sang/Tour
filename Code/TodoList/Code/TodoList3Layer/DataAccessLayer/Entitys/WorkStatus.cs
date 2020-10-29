using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entitys
{
    public partial class WorkStatus
    {
        public WorkStatus()
        {
            Work = new HashSet<Work>();
        }

        [Key]
        public int Id { get; set; }
        [Column("WorkStatus")]
        [StringLength(50)]
        public string WorkStatus1 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [InverseProperty("IdWorkStatusNavigation")]
        public virtual ICollection<Work> Work { get; set; }
    }
}
