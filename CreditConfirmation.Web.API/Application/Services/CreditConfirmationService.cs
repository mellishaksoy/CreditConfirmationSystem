using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CreditConfirmation.Web.API.Application.Dtos;
using CreditConfirmation.Web.API.Infrastructure.Settings;
using CreditConfirmation.Web.API.Models;
using CreditConfirmation.Web.API.Statics;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CreditConfirmation.Web.API.Application.Services
{
    public class CreditConfirmationService : ICreditConfirmationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreditConfirmationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CreditResponseDto> ConfirmCreditForUser(CustomerModel customerModel)
        {
            var httpClient = _httpClientFactory.CreateClient("CreditConfirmationApi");
            
            var response = httpClient.PostAsJsonAsync(CreditApiUrls.CONFIRM_CREDIT, customerModel).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var creditResponseDto = JsonConvert.DeserializeObject<CreditResponseDto>(responseString);
                return creditResponseDto;
            }
            else
            {
                throw new Exception("Error");
            }
            
        }
    }
}
