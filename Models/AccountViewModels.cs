using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.ModelBinding;

namespace MVCDocterPatientSystem.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
    //public class BookingViewModel
    //{
    //    public int Id { get; set; }
    //    public int PatientId { get; set; }
    //    public string PatientName { get; set; }
    //    public int DoctorId { get; set; }
    //    public string DoctorName { get; set; }
    //    public DateTime AppointmentDate { get; set; }
    //    public string Reason { get; set; }
    //    public BookingStatus Status { get; set; }
    //}

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel:PatientInfo
    {
        //[Key]
        //public string Id { get; set; }
        //[Required]
        //public string FirstName { get; set; }

        //[Required]
        //public string LastName { get; set; }

        //[Required]
        ////[DataType(DataType.Date)]
        //public DateTime DateOfBirth { get; set; }

        //[Required]
        //public string Gender { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        //[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid phone number. Must be 10 digits.")]
        [System.Web.Mvc.Remote("IsPhoneNumberAvailabe", "Booking", ErrorMessage = "Phone Number is already exists")]
        public string PhoneNumber { get; set; }

        //[Required]
        //public string History { get; set; }
        //public byte[] Picture { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [System.Web.Mvc.Remote("IsEmailAvailabe", "Booking", ErrorMessage = "Email already exists")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
      
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
       
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
