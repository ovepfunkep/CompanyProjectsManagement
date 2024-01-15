using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(100)]
        public required string Surname { get; set; }
        [MaxLength(100)]
        public string? Patronymic { get; set; }
        [MaxLength(100)]
        public string? Email {  get; set; }

        // Relations
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
