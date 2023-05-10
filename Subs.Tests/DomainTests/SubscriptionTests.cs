using Subs.Api.Domain.Enums;
using Subs.Api.Domain.Products;
using Subs.Api.Domain.Products.ValueObjects;

namespace Subs.Tests.DomainTests
{
    internal class SubscriptionTests
    {
        [Test]
        public void ShouldCreateEmptySub()
        {
            // Arrange
            // Act
            var s = new Subscription();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(s.CreatedAt.Date, Is.EqualTo(DateTime.Now.Date));
                Assert.That(s.Current, Is.Null);
                Assert.That(s.PlanHistory, Is.Empty);
            });
        }

        [Test]
        [TestCase(RecurrencePeriod.Month)]
        [TestCase(RecurrencePeriod.Year)]
        public void ShouldAddPlanToSub(RecurrencePeriod recurrencyPeriod)
        {
            // Arrange
            var subscription = new Subscription();

            var plan = new Plan
            {
                Recurrencies = new List<PlanRecurrency>
                {
                    new PlanRecurrency { Period = RecurrencePeriod.Year },
                    new PlanRecurrency { Period = RecurrencePeriod.Month }
                }
            };

            // Act
            subscription.Add(plan, recurrencyPeriod);

            // Assert
            Assert.That(subscription.Current, Is.Not.Null);
            Assert.That(subscription.Current.Period, Is.EqualTo(recurrencyPeriod));
        }

        [Test]
        public void ShouldUpgrade()
        {
            // Arrange
            var subscription = new Subscription();

            var plan = new Plan
            {
                Recurrencies = new List<PlanRecurrency>
                {
                    new PlanRecurrency { Period = RecurrencePeriod.Year },
                    new PlanRecurrency { Period = RecurrencePeriod.Month }
                }
            };

            // Act
            subscription.Add(plan, RecurrencePeriod.Month);
            subscription.Add(plan, RecurrencePeriod.Year);

            // Assert
            Assert.That(subscription.Current, Is.Not.Null);
            Assert.That(subscription.Current.Period, Is.EqualTo(RecurrencePeriod.Year));
        }
    }
}