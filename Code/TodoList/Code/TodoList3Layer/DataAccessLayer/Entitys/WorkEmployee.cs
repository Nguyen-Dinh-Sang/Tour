using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entitys
{
    [Table("Work_Employee")]
    public partial class WorkEmployee
    {
        [Key]
        public int Id { get; set; }
        public int? IdEmployee { get; set; }
        public int? IdWork { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [ForeignKey(nameof(IdEmployee))]
        [InverseProperty(nameof(Employee.WorkEmployee))]
        public virtual Employee IdEmployeeNavigation { get; set; }
        [ForeignKey(nameof(IdWork))]
        [InverseProperty(nameof(Work.WorkEmployee))]
        public virtual Work IdWorkNavigation { get; set; }
    }
}
