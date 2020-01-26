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
        private AppSettings _settings;

        public CreditConfirmationService(IHttpClientFactory httpClientFactory, IOptions<AppSettings> settings)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings.Value;
        }

        public async Task<CreditResponseDto> ConfirmCreditForUser(CustomerModel customerModel)
        {
            CreditResponseDto creditResponseDto = new CreditResponseDto();

            var httpClient = _httpClientFactory.CreateClient("CreditConfirmationApi");
            
            var response = await httpClient.PostAsJsonAsync(CreditApiUrls.CONFIRM_CREDIT, customerModel);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Error");
            }

            return creditResponseDto;
        }
    }
}
