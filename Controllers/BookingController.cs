using MVCDocterPatientSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web.UI.WebControls;

namespace MVCDocterPatientSystem.Controllers
{
    public class BookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            //var patient = db.patientInfos.FirstOrDefault(p => p.UserId == userId);

            var patient = db.patientInfos.Include(p => p.ApplicationUser)
                                .FirstOrDefault(p => p.UserId == userId);

            if (patient == null)
            {
                // Handle the case where the patient is not found
                return HttpNotFound();
            }
          


            ViewBag.PatientName = patient.FirstName;
            ViewBag.PatientEmail = patient.ApplicationUser.Email;
            ViewBag.PatientPhone = patient.ApplicationUser.PhoneNumber;

            //var bookings = db.appointments.Include(b => b.PatientInfo)
            //                              .Where(b => b.Id == patient.Id)
            //                              .ToList();
            var bookings = db.appointments
                  .Where(b => b.Id == patient.Id)  // Filter by patient Id
                  .ToList();
            //var bookings = db.appointments.Include(b => b.PatientInfo).ToList();
            return View(bookings);
        }

        private ApplicationDbContext context = new ApplicationDbContext();
        [HttpGet]
        public JsonResult IsPhoneNumberAvailabe(string PhoneNumber)
        {
            bool isAvail = !context.Users.Any(u => u.PhoneNumber == PhoneNumber);
            return Json(isAvail, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IsEmailAvailable(string Email)
        {
            bool isAvail = !context.Users.Any(u => u.Email == Email);
            return Json(isAvail, JsonRequestBehavior.AllowGet);
        }
        // GET: Booking
        //public ActionResult Index()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var patient = db.patientInfos.FirstOrDefault(p => p.UserId == userId);

        //    if (patient == null)
        //    {
        //        // Handle the case where the patient is not found
        //        return HttpNotFound();
        //    }

        //    var bookings = db.appointments.Include(b => b.PatientInfo).ToList();
        //    return View(bookings);
        //}



        public ActionResult Create()
        {
            //var patientInfo = new PatientInfo
            //{
            //    FirstName = ,
            //    LastName = "Doe",
            //    Email = "john.doe@example.com",
            //    Phone = "+1234567890"
            //};

           
            return View();
        }


        // POST: Booking/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Appointment appointment)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var patient = db.patientInfos.FirstOrDefault(p => p.UserId == userId);

        //    if (patient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var appointmentDateTime = Date.Date + Time;

        //    //var existingBooking = db.appointments
        //    //    .Where(b => b.Date == Date && b.Status == "Booked")
        //    //    .ToList();

        //    //var isOverlapping = existingBooking.Any(b =>
        //    //    (b.Time >= Time && b.Time < Time.Add(new TimeSpan(1, 0, 0))) ||
        //    //    (b.Time <= Time && b.Time.Add(new TimeSpan(1, 0, 0)) > Time));

        //    //if (isOverlapping)
        //    //{
        //    //    ModelState.AddModelError(nameof(Time), "Another appointment is already booked for this hour.");
        //    //    return View(); // Adjust this to redirect to the appropriate view
        //    //}

        //    var activeBooking = db.appointments.FirstOrDefault(b =>
        //        b.Id == patient.Id && b.Status == "Booked");

        //    if (activeBooking != null)
        //    {
        //        ModelState.AddModelError(nameof(Time), "You already have an active booked appointment.");
        //        return View(); // Adjust this to redirect to the appropriate view
        //    }

        //    //var existingBooking = db.appointments
        //    //   .Where(b => b.AppointmentDate.Date == appointmentDate.Date && b.Status == "Booked")
        //    //   .ToList();
        //    //var startTime = appointmentTime;
        //    //var endTime = appointmentTime.Add(new TimeSpan(1, 0, 0));
        //    //var existingBooking = db.appointments.Any(b => b.appointmentDate == appointmentDate && b.Id == patient.Id && b.Status == "Booked");
        //    //var existingBooking = db.appointments.Any(b =>
        //    //  b.appointmentDate == appointmentDate &&
        //    // b.TimeSpan.Hour == appointmentTime.Hour &&
        //    //     b.Status == "Booked");
        //    //just commnet for inset column date time
        //    //       var existingBooking = db.appointments
        //    //.Where(b => b.appointmentDate == appointmentDate && b.Status == "Booked")
        //    //.ToList(); 


        //    //var isOverlapping = existingBooking.Any(b =>
        //    //  (b.TimeSpan >= startTime && b.TimeSpan < endTime) ||
        //    //  (b.TimeSpan <= startTime && b.TimeSpan.Add(new TimeSpan(1, 0, 0)) > startTime) );

        //    //var existingBooking = db.appointments.Any(b =>
        //    //    b.appointmentDate == appointmentDate &&
        //    //    ((b.TimeSpan >= startTime && b.TimeSpan < endTime) ||
        //    //     (b.TimeSpan <= startTime && b.TimeSpan.Add(new TimeSpan(1, 0, 0)) > startTime)) &&
        //    //    b.Status == "Booked");

        //    //if (isOverlapping)
        //    //{
        //    //    ModelState.AddModelError(nameof(Appointment.TimeSpan), "Another appointment is already booked for this hour.");
        //    //    return View();
        //    //}
        //    //var activeBooking = db.appointments.FirstOrDefault(b =>
        //    //    b.Id == patient.Id &&
        //    //     b.Status == "Booked");

        //    //just commnet for date time add filed
        //    //if (activeBooking != null)
        //    //{
        //    //    ModelState.AddModelError(nameof(Appointment.TimeSpan), "You already have an active booked appointment.");
        //    //    return View();
        //    //}

        //    var newAppointment = new Appointment
        //    {
        //        //Date = Date,
        //        //Time = Time,
        //        Reason = aboutIllness,
        //        Id = patient.Id,
        //        Status = "Pending"
        //    };

        //    //var newAppointment = new Appointment
        //    //{
        //    //    //TimeSpan = appointmentTime,
        //    //    //appointmentDate = appointmentDate,
        //    //    //AppointmentDate = model.Date.Date + model.Time,
        //    //    Reason = aboutIllness,
        //    //    Id = patient.Id,
        //    //    Status = "Pending"
        //    //};

        //    db.appointments.Add(newAppointment);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var patient = db.patientInfos.FirstOrDefault(p => p.UserId == userId);

                if (patient == null)
                {
                    return HttpNotFound();
                }

                // Check if the appointment is within a 1-hour slot
                var selectedDateTime = appointment.AppointmentDateTime;
                var endDateTime = selectedDateTime.AddHours(1); // Add 1 hour

                if (selectedDateTime.Minute != 0 || selectedDateTime.Second != 0 || selectedDateTime.Millisecond != 0)
                {
                    ModelState.AddModelError(nameof(appointment.AppointmentDateTime), "Appointments must start on the hour (e.g., 9:00 AM, 10:00 AM).");
                    return View(appointment);
                }

                // Check for overlapping appointments
                var existingAppointments = db.appointments
                    .Where(a => a.Id == patient.Id && a.Status == "Pending" && a.AppointmentDateTime == appointment.AppointmentDateTime)
                    .ToList();

                if (existingAppointments.Any())
                {
                    //ViewBag.ErrorMessage = "Another appointment is already booked for this date and time.";
                    ModelState.AddModelError(nameof(appointment.AppointmentDateTime), "Another appointment is already booked for this date and time.");
                    //Session["ModelErrors"] = ModelState.ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                    //ModelState.AddModelError( string.Empty ,"Another appointment is already booked for this date and time.");
                    return View(appointment);
                }

                // Check for active booked appointment
                //var activePendingBooking = db.appointments.FirstOrDefault(a => a.PatientId == patient.Id && a.Status == "Pending");
                var activeBooking = db.appointments.FirstOrDefault(a => a.Id == patient.Id && a.Status == "Pending");

                if (activeBooking != null)
                {
                    ModelState.AddModelError(string.Empty, "You already have an active pending appointment.");
                    //Session["ModelErrors"] = ModelState.ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                    //ViewBag.ErrorMessage = "You already have an active pending appointment.";
                    //ModelState.AddModelError(nameof(appointment.AppointmentDateTime), "You already have an active Pending appointment.");
                    return View(appointment);
                }


                // Create new appointment
                appointment.Id = patient.Id;
                appointment.Status = "Pending";

                db.appointments.Add(appointment);
                db.SaveChanges();
                //;TempData["SuccessMessage"] = "Appointment booked successfully."
                return RedirectToAction("Index");
            }

            return View(appointment);
        }



        ////For Booking Create
        //[HttpPost]
        ////[ValidateAntiForgeryToken]


        //public ActionResult Create(appointment patientInfo,DateTime appointmentTime, DateTime appointmentDate, string aboutIlness)
        //{
        //    // Get current user's ID using ASP.NET Identity
        //    var user = User.Identity.GetUserId();
        //    patientInfo.Id = db.patientInfos.FirstOrDefault(p => p.UserId == user)?.Id;
        //    //var patientinfo = db.patientInfos.FirstOrDefault(x => x.UserId == user);
        //    //p/*atientInfo.Status = "Booked";*/
        //    if (patientInfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var existingBooking = db.appointments.Any(b => b.appointmentDate == appointmentDate && b.Id == patientInfo.Id && b.Status == "Booked");
        //    if (existingBooking)
        //    {
        //        ModelState.AddModelError("", "You already have a booking at this time.");
        //        return View(patientInfo); 
        //    }

        //    var data = new appointment
        //    {
        //        TimeSpan = appointmentTime,
        //        appointmentDate = appointmentDate,
        //        Reason = aboutIlness,
        //        Id =patientInfo.Id,
        //        Status = "Pending"
        //    };
        //    //var data = new appointment { TimeSpan = appointmentTime, appointmentDate = appointmentDate, Reason = aboutIlness,Id = patientInfo.Id };
        //    //model.PatientId = patientinfo.Id;
        //    db.appointments.Add(data);
        //    db.SaveChanges();
        //    return View();
        //}


        //public ActionResult Create(appointment patientInfo)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var patient = db.patientInfos.FirstOrDefault(p => p.UserId == userId);
        //    if (patient == null)
        //    {
        //        return HttpNotFound(); 
        //    }
        //    patientInfo.Id = patient.Id;
        //    db.appointments.Add(patientInfo);
        //    db.SaveChanges();

        //    //ViewBag.PatientInfo = patient;
        //    return View();
        //}
        public ActionResult Edit(int? id)
        {
            var booking = db.appointments.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        [HttpPost]
        public ActionResult Edit(Appointment appointment,int id)
        {
            var Exstingcode = db.appointments.Find(id);
            if (ModelState.IsValid)
            {//just commnet for date time add filed
                //Exstingcode.TimeSpan=appointment.TimeSpan;
                //Exstingcode.Reason=appointment.Reason;
                //Exstingcode.appointmentDate=appointment.appointmentDate;
                

                //db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index"); 
            }
            return View(appointment);
        }

        public ActionResult EditProfile()
        {
            // Assuming you have a current logged-in user
            var userId = User.Identity.GetUserId();
            var patient = db.patientInfos.FirstOrDefault(p => p.UserId == userId);

            if (patient == null)
            {
                return HttpNotFound();
            }

            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile( string FirstName, string LastName, HttpPostedFileBase ImageFile)
        {
            var userId = User.Identity.GetUserId();
            var patient = db.patientInfos.FirstOrDefault(p => p.UserId == userId);
            var patientid=patient.Id;
            if (ModelState.IsValid)
            {
                

                if (patient != null)
                {
                    // Update properties based on changes in the model
                    patient.FirstName = FirstName;
                    patient.LastName = LastName;
                    // Update other properties as needed

                    // Handle picture update if a new picture is uploaded
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        using (var binaryReader = new BinaryReader(ImageFile.InputStream))
                        {
                            patient.Image = binaryReader.ReadBytes(ImageFile.ContentLength);
                        }
                    }

                    db.Entry(patient).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home"); // Redirect to a suitable page after editing
                }
                else
                {
                    return HttpNotFound("Patient not found");
                }
            }

            return View();
        }
        // POST: Patient/EditProfile
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditProfile(PatientInfo model, HttpPostedFileBase ImageFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var patient = db.patientInfos.Find(model.Id);

        //        // Update properties based on changes in the model
        //        patient.FirstName = model.FirstName;
        //        patient.LastName = model.LastName;
        //        // Update other properties as needed

        //        //// Handle picture update if a new picture is uploaded
        //        //if (Image != null && Image.ContentLength > 0)
        //        //{
        //        //    using (var binaryReader = new BinaryReader(Image.InputStream))
        //        //    {
        //        //        patient.Image = binaryReader.ReadBytes(Image.ContentLength);
        //        //    }
        //        //}
        //        byte[] imageData = null;
        //        if (ImageFile != null && ImageFile.ContentLength > 0)
        //        {
        //            using (var binaryReader = new BinaryReader(ImageFile.InputStream))
        //            {
        //                imageData = binaryReader.ReadBytes(ImageFile.ContentLength);
        //            }
        //        }

        //        db.Entry(patient).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index", "Home"); // Redirect to a suitable page after editing
        //    }

        //    return View(model);
        //}
        ////for render image
        //public ActionResult RenderImage(int id)
        //{
        //    var patient = db.patientInfos.Find(id);

        //    if (patient == null || patient.Image == null)
        //    {
        //        return new HttpNotFoundResult("Image not found");
        //    }

        //    return File(patient.Image, "image/jpeg");
        //}
    }
}