using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
        [MaxLength(100), MinLength(1)] public string? Email {  get; set; }

        // Relations
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
