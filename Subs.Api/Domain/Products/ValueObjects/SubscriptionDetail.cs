using Subs.Api.Domain.Enums;

namespace Subs.Api.Domain.Products.ValueObjects
{
    public class SubscriptionDetail
    {
        public SubscriptionStage Condition { get; set; } = SubscriptionStage.Inactive;

        public DateOnly TrialEnd { get; set; } = DateOnly.MinValue;

        public DateOnly TrialStart { get; set; } = DateOnly.MinValue;

        public TrialStage IsInTrial(DateTime? dateReference = null)
        {
            dateReference ??= DateTime.Now;

            if (TrialEnd == DateOnly.MinValue)
                return TrialStage.None;

            if (DateOnly.FromDateTime((DateTime)dateReference) < TrialStart)
                return TrialStage.Before;

            if (InTrial((DateTime)dateReference))
                return TrialStage.InTrial;

            return TrialStage.Expired;
        }

        private bool InTrial(DateTime dateReference) => TrialStart <= DateOnly.FromDateTime(dateReference) && TrialEnd >= DateOnly.FromDateTime(dateReference);
    }
}