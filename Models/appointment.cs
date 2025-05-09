using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDocterPatientSystem.Models
{
    public class Appointment
    {
        [Key]
        public int appointmentId { get; set; }
        [Required]
        [Display(Name = "Date Time")]
        public DateTime AppointmentDateTime { get; set; }

        //[Required]
        //[Display(Name = "Date")]
        //public DateTime Date { get; set; }

        //[Required]
        //[Display(Name = "Time")]
        //public TimeSpan Time { get; set; }

        //[Display(Name = "Appointment Date")]

        //public DateTime appointmentDate { get; set; }

        //[DataType(DataType.Time)]
        //public DateTime TimeSpan { get; set; }
        [Required]
        public string Reason { get; set; }

        public string Status { get; set; }

        [ForeignKey("PatientInfo")]
        public string Id { get; set; }
        public virtual PatientInfo PatientInfo { get; set; }
    }
}