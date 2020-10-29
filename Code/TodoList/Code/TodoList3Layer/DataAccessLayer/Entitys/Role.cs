using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entitys
{
    public partial class Role
    {
        public Role()
        {
            Employee = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Role")]
        [StringLength(50)]
        public string Role1 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [InverseProperty("IdRoleNavigation")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
