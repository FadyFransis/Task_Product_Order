using App.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace App.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ForgetPasswordCode { get; set; }
        public string ActivationCode { get; set; }
        public string SocialLoginId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
