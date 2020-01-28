using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Settings
{
    public class MailSettings
    {
        public string From { get; set; }
        public string To { get; set; }
        public string BodyConfirmed { get; set; }
        public string BodyDenied { get; set; }
        public string Password { get; set; }
    }
}
