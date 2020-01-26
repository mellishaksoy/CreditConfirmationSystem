using CreditConfirmation.API.Application.Dtos;

namespace CreditConfirmation.API.Application.Services
{
    public interface ICreditConfirmationService
    {
        void AddUser(UserInformationDto userInformationDto);
    }
}
