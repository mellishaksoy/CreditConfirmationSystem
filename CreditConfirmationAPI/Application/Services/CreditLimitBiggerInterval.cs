using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Application.Services
{
    public class CreditLimitBiggerInterval : CreditStrategy
    {
        public override double Calculate(double income, int multiplier = 1)
        {
            return income * multiplier;
        }
    }
}
