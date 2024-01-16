using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100), Index(IsUnique = true)]
        public required string Name { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public int Priority { get; set; }

        // Relations
        public required virtual int ManagerID { get; set; }
        public required virtual Employee Manager { get; set; }
        public required virtual int CustomerCompanyID { get; set; }
        public required virtual Company CustomerCompany { get; set; }
        public required virtual int ContractorCompanyID { get; set; }
        public required virtual Company ContractorCompany { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
