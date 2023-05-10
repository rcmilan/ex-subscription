using Subs.Api.Domain.Enums;
using Subs.Api.Domain.Products;
using Subs.Api.Domain.Products.ValueObjects;

namespace Subs.Tests.DomainTests
{
    internal class PlanTests
    {
        [Test]
        [TestCase(RecurrencePeriod.Month)]
        [TestCase(RecurrencePeriod.Year)]
        public void ShouldGetRecurrency(RecurrencePeriod period)
        {
            // Arrange
            var p = new Plan
            {
                Recurrencies = new List<PlanRecurrency>
                {
                    new PlanRecurrency { Period = RecurrencePeriod.Year },
                    new PlanRecurrency { Period = RecurrencePeriod.Month },
                }
            };

            // Act
            var r = p.GetRecurrency(period);

            // Assert
            Assert.That(r.Period, Is.EqualTo(period));
        }
    }
}