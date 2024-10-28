using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Specialty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [MaxLength(400)]
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public IEnumerable<Doctor> SpecialtyDoctors { get; set; } = new List<Doctor>();

        public Specialty() 
        {
            Status = true;
        }
    }
}
