using DataAccessHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Entities
{
    public class CreditApplication : MongoEntity
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public double Income { get; set; }
        public int Score { get; set; }
        public bool Confirmed { get; set; }
        public double ConfirmedLimit { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
