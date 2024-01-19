using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Project
    {
        [Key] public int ID { get; set; }
        [Required, MaxLength(100), MinLength(1)] public required string Name { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateEnded { get; set; }
        public int? Priority { get; set; }

        // Foreign keys
        [Required] 
        public required int ManagerID { get; set; }
        public required int CustomerCompanyID { get; set; }
        public required int ContractorCompanyID { get; set; }

        // Navigation
        public virtual Employee? Manager { get; set; }
        public virtual Company? CustomerCompany { get; set; }
        public virtual Company? ContractorCompany { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
