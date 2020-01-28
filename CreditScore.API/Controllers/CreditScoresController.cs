using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditScore.API.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CreditScore.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CreditScoresController : ControllerBase
    {

        private readonly ICreditScoreService _creditScoreService;

        public CreditScoresController(ICreditScoreService creditScoreService)
        {
            _creditScoreService = creditScoreService;
        }


        // GET api/values/5
        [HttpGet("{identityNumber}")]
        public ActionResult<int> Get(string identityNumber)
        {
            var score = _creditScoreService.GetUserScore(identityNumber);
            return Ok(score);
        }
        
    }
}
