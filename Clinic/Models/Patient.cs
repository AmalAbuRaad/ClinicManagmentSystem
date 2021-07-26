using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Patient
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [StringLength(50, ErrorMessage = "Maximum Length is {1}")]
        [RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Maximum Length is {1}")]
        [RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string Address { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? RegDate { get; set; }
        [RegularExpression("^\\d{9}$")]
        public string SSN { get; set; }
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Country { get; set; }
        public List<MedicalHistory> MedicalHistories { get; set; }
        public List<Appointment> Appointments { get; set; }
        [NotMapped]
        public string PatientName
        {
            get { return $"{FirstName} {LastName}"; }
            set { PatientName = "amal"; }
        }
        
    }
}
