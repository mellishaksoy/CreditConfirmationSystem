﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.Web.API.Models
{
    public class CustomerModel
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public double Income { get; set; }
        public string Email { get; set; }
    }
}
