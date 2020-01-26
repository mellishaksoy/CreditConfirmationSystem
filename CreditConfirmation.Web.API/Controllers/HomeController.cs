using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreditConfirmation.Web.API.Models;
using CreditConfirmation.Web.API.Application.Services;

namespace CreditConfirmation.Web.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICreditConfirmationService _creditConfirmationService;

        public HomeController(ICreditConfirmationService creditConfirmationService)
        {
            _creditConfirmationService = creditConfirmationService;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CalculateCredit(CustomerModel customerModel)
        {
            await _creditConfirmationService.ConfirmCreditForUser(customerModel);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
