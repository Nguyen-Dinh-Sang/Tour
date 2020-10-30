using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entitys
{
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }
        public int? IdEmployee { get; set; }
        public int? IdWork { get; set; }
        [StringLength(200)]
        public string CommentContent { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [ForeignKey(nameof(IdEmployee))]
        [InverseProperty(nameof(Employee.Comment))]
        public virtual Employee IdEmployeeNavigation { get; set; }
        [ForeignKey(nameof(IdWork))]
        [InverseProperty(nameof(Work.Comment))]
        public virtual Work IdWorkNavigation { get; set; }
    }
}
