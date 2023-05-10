using Subs.Api.Domain.Billing;

namespace Subs.Tests.DomainTests
{
    internal class PaymentTests
    {
        [Test]
        public void ShouldCheckIfOnDateOnValidRange_Expired()
        {
            // Arrange
            // Act
            var p = new Payment(100, new DateOnly(2000, 1, 1));

            // Assert
            Assert.That(p.IsExpired);
        }

        [Test]
        public void ShouldCheckIfOnDateOnValidRange_NotExpired()
        {
            // Arrange
            // Act
            var p = new Payment(100, new DateOnly(3000, 1, 1));

            // Assert
            Assert.That(!p.IsExpired);
        }

        [Test]
        public void ShouldPayOnValidRange_Expired()
        {
            // Arrange
            var p = new Payment(100, new DateOnly(2000, 1, 1));

            // Act
            p.Pay();
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(!p.IsPaid);
                Assert.That(p.PaidAt, Is.EqualTo(DateTime.MinValue));
            });
        }

        [Test]
        public void ShouldPayOnValidRange_NotExpired()
        {
            // Arrange
            var p = new Payment(100, new DateOnly(3000, 1, 1));

            // Act
            p.Pay();
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(p.IsPaid);
                Assert.That(p.PaidAt, Is.GreaterThan(DateTime.MinValue));
            });
        }
    }
}