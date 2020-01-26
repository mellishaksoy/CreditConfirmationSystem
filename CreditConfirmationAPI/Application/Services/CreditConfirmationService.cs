using CreditConfirmation.API.Application.Dtos;
using CreditConfirmation.API.Domain;
using CreditConfirmation.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Application.Services
{
    public class CreditConfirmationService : ICreditConfirmationService
    {
        private readonly IUserRepository _userRepository;

        public CreditConfirmationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void AddUser(UserInformationDto userInformationDto)
        {
            UserEntity user = new UserEntity();
            user.Email = "melis_sevil_aksoy@hotmail.com";
            user.PhoneNumber = "5392690882";
            user.IdentityNumber = "47506281774";
            user.Name = "Melis Sevil";
            user.LastName = "Aksoy";

            _userRepository.Add(user);
        }
    }
}
