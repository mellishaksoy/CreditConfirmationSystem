using CreditConfirmation.API.Application.Dtos;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Application.Services
{
    public interface ICreditConfirmationService
    {
        Task<ConfirmationResultDto> ConfirmUserCredit(UserInformationDto userInformationDto);
    }
}
