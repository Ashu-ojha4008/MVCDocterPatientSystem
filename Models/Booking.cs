using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDocterPatientSystem.Models
{
    public enum BookingStatus
    {
        Processing,
        Approved,
        Cancelled,
        Completed
    }

    public class Booking
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public virtual ApplicationUser Patient { get; set; }
        public int DoctorId { get; set; }
        public virtual ApplicationUser Doctor { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Processing;
    }
}