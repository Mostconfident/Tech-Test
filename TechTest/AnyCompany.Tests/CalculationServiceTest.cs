using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Services;
using NUnit.Framework;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class CalculationServiceTest
    {
        private ICalculationService calculationService;
        [SetUp]
        public void Setup()
        {

            this.calculationService = new CalculationService();
        }

        [TestCase("SA", 0)]
        [TestCase("USA", 0)]
        [TestCase("IND", 0)]
        public void Should_Return_Zero_Vat(string countryCode, double expected)
        {
            var vat = calculationService.GetVatByCountryCode(countryCode);

            Assert.AreEqual(expected, vat);
        }

        [TestCase("UK", 0.2d)]
        public void Should_Return_Vat(string countryCode, double expected)
        {
            var vat = calculationService.GetVatByCountryCode(countryCode);

            Assert.AreEqual(expected, vat);
        }
    }
}
