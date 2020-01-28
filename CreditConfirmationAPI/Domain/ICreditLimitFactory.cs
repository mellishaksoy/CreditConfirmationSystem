using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Domain
{
    public interface ICreditLimitFactory
    {
        double CalculateCreditLimit(int score, double income);
    }
}
