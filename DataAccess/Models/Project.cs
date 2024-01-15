using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public int Priority { get; set; }

        // Relations
        public required int ManagerID { get; set; }
        public required Employee Manager { get; set; }
        public required int CustomerCompanyID { get; set; }
        public required Company CustomerCompany { get; set; }
        public required int ContractorCompanyID { get; set; }
        public required Company ContractorCompany { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
