using MVCDocterPatientSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVCDocterPatientSystem.Controllers
{
    [Authorize(Roles ="doctor")]
    public class DoctorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var patients = db.patientInfos.Include(p => p.ApplicationUser).OrderByDescending(p=>p.Id).ToList();

            return View(patients);

        }

        public ActionResult Appointments()
        {
            var bookings = db.appointments.Include(b => b.PatientInfo).OrderByDescending(a=>a.appointmentId).ToList();
            return View(bookings);
        }
        

        //public ActionResult Appointments()
        //{
        //    var appointments = db.appointments.ToList(); 
        //    return View(appointments); 
        //}

        // POST: Booking/ChangeStatus
        [HttpPost]
        //[Authorize(Roles = "Doctor")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int id, string status)
        {
            var appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            if (status == "Approve")
            {
                appointment.Status = "Approved";
            }
            else if (status == "Reject")
            {
                appointment.Status = "Rejected";
            }
            else if (status == "Complete")
            {
                appointment.Status = "Completed";
            }

            db.SaveChanges();
            return RedirectToAction("Appointments");
        }


        //// POST: Doctor/ChangeBookingStatus/5
        //[HttpPost]
        //public ActionResult ChangeBookingStatus(int id)
        //{
        //    var appointment = db.appointments.Find(id);
        //    if (appointment == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    // Toggle status between 'Booked' and 'Completed'
        //    appointment.Status = appointment.Status == "Booked" ? "Completed" : "Booked";
        //    db.SaveChanges();

        //    return RedirectToAction("Appointments");
        //}

        //public ActionResult ChangeBookingStatus(int id)
        //{
        //    var booking = db.appointments.Find(id);
        //    if (booking != null)
        //    {
        //        booking.Status = booking.Status == "Booked" ? "Completed" : "Booked";
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Bookings");
        //}

        public ActionResult BlockUnblockPatient(string id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                user.IsBlocked = !user.IsBlocked;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetPatientDetails(int id)
        {
            var booking = db.appointments.Include(b => b.PatientInfo).Include(b => b.PatientInfo.ApplicationUser).FirstOrDefault(b => b.appointmentId==id);

            if (booking == null)
            {
                return HttpNotFound();
            }
            //var base64String = Convert.ToBase64String(booking.PatientInfo.Picture);

            var patientDetails = new
            {
                booking.PatientInfo.FirstName,
                booking.PatientInfo.LastName,
                booking.PatientInfo.History,
                Image = booking.PatientInfo.Image,
                booking.PatientInfo.ApplicationUser.Email,
                booking.PatientInfo.ApplicationUser.PhoneNumber
            };

            return Json(patientDetails, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CalendarView()
        {
            return View();
        }
        public JsonResult GetAppointments()
        {
            var appointments = db.appointments
                .Include(a => a.PatientInfo)
                .ToList()
                .Select(a => new
                {
                    id = a.appointmentId,
                    title = a.Reason,
                    start = a.AppointmentDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = a.AppointmentDateTime.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss"),
                    status = a.Status,
                    patient = a.PatientInfo.FirstName + " " + a.PatientInfo.LastName
                });

            return Json(appointments, JsonRequestBehavior.AllowGet);
        }


        // GET: Doctor/AppointmentsData
        public ActionResult AppointmentsData()
        {
            var appointments = db.appointments.ToList();
            var model = appointments.Select(a => new Appointment
            {
                AppointmentDateTime = a.AppointmentDateTime,
                Reason = a.Reason,
                //PatientInfo = a.PatientInfo.FirstName,
                //Email = a.Patient.Email,
                //Status = a.Status
            });

            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

    }
}