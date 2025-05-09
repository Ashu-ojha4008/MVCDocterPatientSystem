using MVCDocterPatientSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVCDocterPatientSystem.Controllers
{
    public class ModelBindingDTController : Controller
    {
        // GET: ModelBindingDT
        private ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Appointments()
        {
            return View();
        }

        public ActionResult GetAppointments()
        {
            var appointments = _context.appointments.Include(a => a.PatientInfo.ApplicationUser).ToList();
            var data = appointments.Select(a => new
            {
                a.appointmentId,
                a.AppointmentDateTime,
                a.Reason,
                PatientName = a.PatientInfo.FirstName + " " + a.PatientInfo.LastName,
                a.PatientInfo.ApplicationUser.Email,
                a.Status
            }).ToList();

            return View(data);
        }


        public JsonResult GetAppointmentsData()
        {
            var appointments = _context.appointments.Include(a => a.PatientInfo.ApplicationUser).ToList();
            var data = appointments.Select(a => new
            {
                a.appointmentId,
                a.AppointmentDateTime,
                a.Reason,
                PatientName = a.PatientInfo.FirstName + " " + a.PatientInfo.LastName,
                a.PatientInfo.ApplicationUser.Email,
                a.Status
            }).ToList();

            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPatientDetails(int id)
        {
            var appointment = _context.appointments.Include(a => a.PatientInfo).FirstOrDefault(a => a.appointmentId == id);
            if (appointment == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                FirstName = appointment.PatientInfo.FirstName,
                LastName = appointment.PatientInfo.LastName,
                Email = appointment.PatientInfo.ApplicationUser.Email,
                PhoneNumber = appointment.PatientInfo.ApplicationUser.PhoneNumber,
                History = appointment.PatientInfo.History,
                Image = appointment.PatientInfo.Image
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int id, string status)
        {
            var appointment = _context.appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = status;
                _context.SaveChanges();
            }
            return RedirectToAction("Appointments");
        }
    }
}