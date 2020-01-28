using CreditConfirmation.API.Application.Dtos;
using CreditConfirmation.API.Domain;
using CreditConfirmation.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using System.Net.Http;
using CreditConfirmation.API.Statics;
using CreditConfirmation.API.Infrastructure.Extensions;
using System.Net.Mail;
using System.Net;
using CreditConfirmation.API.Settings;
using Microsoft.Extensions.Options;

namespace CreditConfirmation.API.Application.Services
{
    public class CreditConfirmationService : ICreditConfirmationService
    {
        private readonly ICreditApplicationRepository _creditApplicationRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICreditLimitFactory _creditLimitFactory;
        private readonly AppSettings _appSettings;

        public CreditConfirmationService(IHttpClientFactory httpClientFactory, ICreditLimitFactory creditLimitFactory,
            ICreditApplicationRepository creditApplicationRepository, IOptions<AppSettings> appSettings)
        {
            _creditApplicationRepository = creditApplicationRepository;
            _httpClientFactory = httpClientFactory;
            _creditLimitFactory = creditLimitFactory;
            _appSettings = appSettings.Value;
        }
        public async Task<ConfirmationResultDto> ConfirmUserCredit(UserInformationDto userInformationDto)
        {
            if (userInformationDto.IdentityNumber == null || !userInformationDto.IdentityNumber.ValidateIdentityNumber())
            {
                throw new Exception("Identity Number is not true format");
            }

            var creditApplicationEntity = userInformationDto.Adapt<CreditApplication>();

            var httpClient = _httpClientFactory.CreateClient("CreditScore.API");

            var response = await httpClient.GetAsync(string.Format(CreditScoreApiUrls.GET_SCORE, creditApplicationEntity.IdentityNumber));

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                int.TryParse(responseBody, out var score);

                var limit = _creditLimitFactory.CalculateCreditLimit(score, userInformationDto.Income);

                creditApplicationEntity.Score = score;
                creditApplicationEntity.ConfirmedLimit = limit;
                creditApplicationEntity.InsertDate = DateTime.Now;

                // save to db
                await _creditApplicationRepository.AddAsync(creditApplicationEntity);

                // send sms

                //send e-mail
                if (limit > 0)
                {
                    var body = string.Format(_appSettings.MailSettings.BodyConfirmed, userInformationDto.FirstName, limit + " TL");
                    body.SendEmail(_appSettings.MailSettings.From, _appSettings.MailSettings.To, _appSettings.MailSettings.Password);
                }
                else
                {
                    _appSettings.MailSettings.BodyDenied.SendEmail(_appSettings.MailSettings.From, _appSettings.MailSettings.To, _appSettings.MailSettings.Password);
                }


                // return response dto
                return new ConfirmationResultDto
                {
                    Confirmed = limit > 0,
                    Limit = limit + "TL"
                };
            }
            else
            {
                throw new Exception("Score Service Error");
            }


        }
    }
}
