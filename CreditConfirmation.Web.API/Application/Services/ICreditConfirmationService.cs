using CreditConfirmation.Web.API.Application.Dtos;
using CreditConfirmation.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.Web.API.Application.Services
{
    public interface ICreditConfirmationService
    {
        Task<CreditResponseDto> ConfirmCreditForUser(CustomerModel customerModel);
    }
}
