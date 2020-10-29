using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entitys
{
    public partial class Employee
    {
        public Employee()
        {
            Comment = new HashSet<Comment>();
            WorkEmployee = new HashSet<WorkEmployee>();
            WorkListEmployee = new HashSet<WorkListEmployee>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        public int? IdRole { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [ForeignKey(nameof(IdRole))]
        [InverseProperty(nameof(Role.Employee))]
        public virtual Role IdRoleNavigation { get; set; }
        [InverseProperty("IdEmployeeNavigation")]
        public virtual ICollection<Comment> Comment { get; set; }
        [InverseProperty("IdEmployeeNavigation")]
        public virtual ICollection<WorkEmployee> WorkEmployee { get; set; }
        [InverseProperty("IdEmployeeNavigation")]
        public virtual ICollection<WorkListEmployee> WorkListEmployee { get; set; }
    }
}
