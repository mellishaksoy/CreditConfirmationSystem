using System;

namespace CreditConfirmation.API.Application.Services
{
    public class CreditLimitBetweenInterval : CreditStrategy
    {
        public override double Calculate(double income, int multiplier = 1)
        {
            //Kredi skoru 500 puan ile 1000 puan arasında ise ve aylık geliri 5000 TL’nin altında ise
            //kullanıcının kredi başvurusu onaylanır ve kullanıcıya 10.000 TL limit atanır.
            if (income < 5000)
                return 10000;
            else
                throw new Exception("Bu case için bir hesaplama kuralı pdf te yazmıyordu :) ");
        }
    }
}
