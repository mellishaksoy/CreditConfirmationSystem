using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditConfirmation.API.Application.Dtos;
using CreditConfirmation.API.Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreditConfirmation.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfirmationsController : Controller
    {
        private readonly ICreditConfirmationService _creditConfirmationService;
        public ConfirmationsController(ICreditConfirmationService creditConfirmationService)
        {
            _creditConfirmationService = creditConfirmationService;
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCredit([FromBody] UserInformationDto userInformationDto)
        {
            var result = await _creditConfirmationService.ConfirmUserCredit(userInformationDto);
            return Ok(result);
        }
    }
}
