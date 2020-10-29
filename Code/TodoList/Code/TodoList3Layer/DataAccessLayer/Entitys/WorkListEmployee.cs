using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entitys
{
    [Table("WorkList_Employee")]
    public partial class WorkListEmployee
    {
        [Key]
        public int Id { get; set; }
        public int? IdEmployee { get; set; }
        public int? IdWorkList { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [ForeignKey(nameof(IdEmployee))]
        [InverseProperty(nameof(Employee.WorkListEmployee))]
        public virtual Employee IdEmployeeNavigation { get; set; }
        [ForeignKey(nameof(IdWorkList))]
        [InverseProperty(nameof(WorkList.WorkListEmployee))]
        public virtual WorkList IdWorkListNavigation { get; set; }
    }
}
