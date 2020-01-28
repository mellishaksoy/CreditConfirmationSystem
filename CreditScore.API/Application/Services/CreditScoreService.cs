using CreditScore.API.Domain;
using CreditScore.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScore.API.Application.Services
{
    public class CreditScoreService : ICreditScoreService
    {
        private readonly List<int> RandomScoreIntervalList = new List<int> { 1, 2, 3 };
        public CreditScoreService()
        {

        }

        public int GetUserScore(string identityNumber)
        {
            var random = new Random();
            var scoreIntervalIndex = random.Next(0, RandomScoreIntervalList.Count);
            var scoreInterval = RandomScoreIntervalList[scoreIntervalIndex];

            var score = 0;
            switch ((ScoreIntervals)scoreInterval)
            {
                case ScoreIntervals.ScoreUnder500:
                    score = random.Next(0, 500);
                    break;

                case ScoreIntervals.ScoreBetween500And1000:
                    score = random.Next(500, 1000);
                    break;

                case ScoreIntervals.ScoreBiggerThan1000:
                    score = random.Next(1000, int.MaxValue);
                    break;
            }

            return score;
        }
    }
}
