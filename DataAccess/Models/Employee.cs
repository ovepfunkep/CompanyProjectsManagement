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
        [Key] public int ID { get; set; }
        [Required, MaxLength(100), MinLength(1)] public required string Name { get; set; }
        [Required, MaxLength(100), MinLength(1)] public required string Surname { get; set; }
        [MaxLength(100), MinLength(1)] public string? Patronymic { get; set; }
        [MaxLength(100), MinLength(1)] public string? Email { get; set; }

        // Foreign keys
        [Required] public required int CompanyID { get; set; }

        // Navigation
        public virtual Company? Company { get; set; }
        public virtual ICollection<Project> ParticipatedProjects { get; set; } = new HashSet<Project>();
        public virtual ICollection<Project> ManagedProjects { get; set; } = new HashSet<Project>();
    }
}
