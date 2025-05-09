using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCDocterPatientSystem.Models
{
    public class CustomUserValidator : UserValidator<ApplicationUser>
    {
        public CustomUserValidator(UserManager<ApplicationUser, string> manager) : base(manager)
        {
        }
        public  override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            var result= await base.ValidateAsync(user);
            if (user.IsBlocked)
            {
                var errors = result.Errors.ToList();
                errors.Add("Your account has been blocked. Please contact support.");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }

}