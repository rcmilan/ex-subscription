using Subs.Api.Domain.Enums;

namespace Subs.Api.Domain.Products.ValueObjects
{
    public class SubscriptionDetail
    {
        public SubscriptionDetail()
        {
        }

        public SubscriptionDetail(int trialDays) : this()
        {
            TrialStart = DateOnly.FromDateTime(DateTime.Now);
            TrialEnd = TrialStart.AddDays(trialDays);

            Condition = SubscriptionStage.Trial;
        }

        public SubscriptionStage Condition { get; set; } = SubscriptionStage.Inactive;
        public bool InTrial => TrialStart <= DateOnly.FromDateTime(DateTime.Now) && TrialEnd >= DateOnly.FromDateTime(DateTime.Now);
        public DateOnly TrialEnd { get; init; }
        public DateOnly TrialStart { get; init; }
    }
}