using Subs.Api.Domain.Enums;
using Subs.Api.Domain.Products;

namespace Subs.Tests.DomainTests
{
    internal class TrialTests
    {
        [Test]
        [TestCase(1)]
        [TestCase(7)]
        public void CreateSub_AddFutureTrial(int trialDays)
        {
            // Arrange
            var sub = new Subscription();

            // Act
            sub.UpsertTrial(trialDays, DateTime.Now.AddDays(99));

            // Assert
            Assert.That(sub.Detail.IsInTrial(), Is.EqualTo(TrialStage.Before));
        }

        [Test]
        [TestCase(1)]
        [TestCase(7)]
        public void CreateSub_AddPastTrial(int trialDays)
        {
            // Arrange
            var sub = new Subscription();

            // Act
            sub.UpsertTrial(trialDays, DateTime.Now.AddDays(-99));

            // Assert
            Assert.That(sub.Detail.IsInTrial(), Is.EqualTo(TrialStage.Expired));
        }

        [Test]
        [TestCase(1)]
        [TestCase(7)]
        public void CreateSub_CheckFutureTrial(int trialDays)
        {
            // Arrange
            var sub = new Subscription();

            var addDays = DateTime.Now.AddDays(99);

            // Act
            sub.UpsertTrial(trialDays, addDays);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sub.Detail.IsInTrial(addDays), Is.EqualTo(TrialStage.InTrial));
                Assert.That(sub.Detail.Condition, Is.EqualTo(SubscriptionStage.Trial));
            });
        }
    }
}