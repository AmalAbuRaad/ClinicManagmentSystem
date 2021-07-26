using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Appointment
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        public long DoctorId { get; set; }
        [ForeignKey(name: "DoctorId")]
        public Doctor Doctor { get; set; }
        [ForeignKey(name: "PatientId")]
        public Patient Patient { get; set; }
        [Required]
        public long PatientId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Reservation { get; set; }
        public long AppointmentTypeId { get; set; }
        [ForeignKey(name: "AppointmentTypeId")]
        [Display(Name = "Appointment Type")]
        public AppointmentType Type { get; set; }
    }
}
