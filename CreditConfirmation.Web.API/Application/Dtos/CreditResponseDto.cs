﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.Web.API.Application.Dtos
{
    public class CreditResponseDto
    {
        public bool Confirmed { get; set; }
        public string Limit { get; set; }
    }
}
