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
        public async Task<IActionResult> Index(CustomerModel customerModel)
        {
            var result = await _creditConfirmationService.ConfirmCreditForUser(customerModel);
            if(result == null)
            {
                ViewData["Message"] = "An error occured. Please try again.";
                return View();
            }

            ViewData["Message"] = "Credit Confirmation Result ";
            ViewData["Confirmed"] = "Your credit application is ";

            if (result.Confirmed)
            {
                ViewData["Confirmed"] += "confirmed.";
                ViewData["Limit"] = "Confirmed Limit is " + result.Limit;
            }
            else
            {
                ViewData["Confirmed"] += "denied.";
                ViewData["Limit"] = "";
            }
            return View("Index");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
