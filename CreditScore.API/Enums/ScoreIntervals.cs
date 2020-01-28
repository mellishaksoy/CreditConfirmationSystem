using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScore.API.Enums
{
    public enum ScoreIntervals
    {
        ScoreUnder500 = 1,
        ScoreBetween500And1000 = 2,
        ScoreBiggerThan1000 = 3
    }
}
