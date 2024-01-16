using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Company
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100), Index(IsUnique = true)]
        public required string Name { get; set; }

        // Relations
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        public virtual ICollection<Project> OrderedProjects { get; set; } = new HashSet<Project>();
        public virtual ICollection<Project> MadenProjects { get; set; } = new HashSet<Project>();
    }
}
