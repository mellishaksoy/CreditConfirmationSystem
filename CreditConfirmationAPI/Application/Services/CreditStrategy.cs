using CreditConfirmation.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Application.Services
{
    /// <summary>
    /// Strategy abstract class
    /// </summary>
    public abstract class CreditStrategy
    {
        public abstract double Calculate( double income, int multiplier = 1);
    }
}
