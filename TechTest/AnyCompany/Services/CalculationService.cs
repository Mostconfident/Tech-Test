using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Services
{
    public class CalculationService : ICalculationService
    {
        public double GetVatByCountryCode(string countryCode)
        {
            if (countryCode == "UK") return 0.2d;

            return 0;
        }
    }
}
