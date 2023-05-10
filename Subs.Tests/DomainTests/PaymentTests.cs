using Subs.Api.Domain.Finance;

namespace Subs.Tests.DomainTests
{
    internal class PaymentTests
    {
        [Test]
        public void ShouldCheckIfOnDateOnValidRange_Expired()
        {
            // Arrange
            // Act
            var p = new Payment(100, new DateOnly(2000, 1,1));

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
            Assert.That(p.IsExpired);
        }
    }
}
