using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class MedicalHistory
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Display(Name = "Patient")]
        public long PatientId { get; set; }
        [ForeignKey(name: "PatientId")]
        public Patient Patient { get; set; }
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Maximum Length is {1}")]
        public string Description { get; set; }
    }
}
