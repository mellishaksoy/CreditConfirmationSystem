using CreditConfirmation.API.Domain;
using CreditConfirmation.API.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Application.Services
{
    public class CreditLimitFactory : ICreditLimitFactory
    {
        private readonly AppSettings _appSettings;
        public CreditLimitFactory(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            if (_appSettings == null)
                _appSettings = new AppSettings();
        }
        public double CalculateCreditLimit(int score, double income)
        {
            double limit = 0;
            switch (score)
            {
                case int n when (n < _appSettings.CreditLimitSettings.FirstScoreInterval):
                    limit = 0;
                break;
                case int n when (n < _appSettings.CreditLimitSettings.SecondScoreInterval && n >= _appSettings.CreditLimitSettings.FirstScoreInterval):
                    limit = new CreditLimitBetweenInterval().Calculate(income);
                    break;

                case int n when (n >= _appSettings.CreditLimitSettings.SecondScoreInterval):
                    limit = new CreditLimitBiggerInterval().Calculate(income, _appSettings.CreditLimitSettings.LimitMultiplier);
                    break;
            }

            return limit;
        }
    }
}
