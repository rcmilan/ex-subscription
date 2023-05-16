using Subs.Api.Domain.Enums;

namespace Subs.Api.Domain.Products.ValueObjects
{
    public class SubscriptionDetail
    {
        public SubscriptionStage Condition { get; set; } = SubscriptionStage.Inactive;

        public bool InTrial(DateTime? dateReference = null)
        {
            dateReference ??= DateTime.Now;

            return TrialStart <= DateOnly.FromDateTime((DateTime)dateReference) && TrialEnd >= DateOnly.FromDateTime((DateTime)dateReference);
        }

        public DateOnly TrialEnd { get; set; } = DateOnly.MinValue;

        public DateOnly TrialStart { get; set; } = DateOnly.MinValue;
    }
}