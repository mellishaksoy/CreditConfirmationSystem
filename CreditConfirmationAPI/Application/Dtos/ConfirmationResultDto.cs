using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Application.Dtos
{
    public class ConfirmationResultDto
    {
        public bool Confirmed { get; set; }
        public string Limit { get; set; }
    }
}
