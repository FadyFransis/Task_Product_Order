using App.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities
{
    public class UserAddressBook : BaseEntity
    {
        public string Title { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AdditionalAddress { get; set; }
        public string PostalCode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public bool IsDefaultAddress { get; set; }
    }
}
