using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Company
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }

        // Relations
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
