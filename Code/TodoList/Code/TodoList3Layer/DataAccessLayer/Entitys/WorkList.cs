using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entitys
{
    public partial class WorkList
    {
        public WorkList()
        {
            Work = new HashSet<Work>();
            WorkListEmployee = new HashSet<WorkListEmployee>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string WorkListName { get; set; }
        public int? IdWorkListStatus { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [ForeignKey(nameof(IdWorkListStatus))]
        [InverseProperty(nameof(WorkListStatus.WorkList))]
        public virtual WorkListStatus IdWorkListStatusNavigation { get; set; }
        [InverseProperty("IdWorkListNavigation")]
        public virtual ICollection<Work> Work { get; set; }
        [InverseProperty("IdWorkListNavigation")]
        public virtual ICollection<WorkListEmployee> WorkListEmployee { get; set; }
    }
}
