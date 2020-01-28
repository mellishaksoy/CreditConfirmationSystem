using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScore.API.Domain
{
    public interface ICreditScoreService
    {
        int GetUserScore(string identityNumber);
    }
}
