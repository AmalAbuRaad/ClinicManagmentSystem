using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class AppointmentType
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [StringLength(50, ErrorMessage = "Maximum Length is {1}")]
        public string Type { get; set; }
        
    }
}
