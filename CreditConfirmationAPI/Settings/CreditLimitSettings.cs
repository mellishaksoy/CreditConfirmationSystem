using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Settings
{
    public class CreditLimitSettings
    {
        public int LimitMultiplier { get; set; }
        public double DefaultLimit { get; set; }
        public double IncomeLimit { get; set; }
        public int FirstScoreInterval { get; set; }
        public int SecondScoreInterval { get; set; }

        public CreditLimitSettings()
        {
            LimitMultiplier = 4;
            DefaultLimit = 10000;
            IncomeLimit = 5000;
            FirstScoreInterval = 500;
            SecondScoreInterval = 1000;
        }
    }
}
