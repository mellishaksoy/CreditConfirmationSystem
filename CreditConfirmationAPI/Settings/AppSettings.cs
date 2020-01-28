using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Settings
{
    public class AppSettings
    {
        public CreditLimitSettings CreditLimitSettings { get; set; }
        public DbSettings DbSettings { get; set; }
        public SwaggerConfigurationInfo SwaggerConfigurationInfo { get; set; }
        public MailSettings MailSettings { get; set; }
        public string CreditScoreServiceUrl { get; set; }

        public AppSettings()
        {
            CreditLimitSettings = new CreditLimitSettings();
        }
    }

}
