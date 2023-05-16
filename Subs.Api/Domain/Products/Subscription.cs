using Subs.Api.Domain.Base;
using Subs.Api.Domain.Enums;
using Subs.Api.Domain.Products.ValueObjects;
using static Subs.Api.Domain.Products.ValueObjects.SubscriptionPlan;

namespace Subs.Api.Domain.Products
{
    public class Subscription : BaseEntity<Guid>, IAggregateRoot
    {
        public DateTime CreatedAt { get; init; } = DateTime.Now;
        public SubscriptionPlan? Current => GetCurrent(PlanHistory);
        public virtual SubscriptionDetail Detail { get; init; } = new SubscriptionDetail();
        public virtual ICollection<SubscriptionPlan> PlanHistory { get; set; } = new List<SubscriptionPlan>();

        public Subscription Add(Plan plan, RecurrencePeriod selectedRecurrencyPeriod) => Add(plan, plan.GetRecurrency(selectedRecurrencyPeriod));

        public Subscription Add(Plan plan, PlanRecurrency selectedRecurrency)
        {
            if (!plan.Recurrencies.Contains(selectedRecurrency)) return this;

            PlanHistory.Add(new SubscriptionPlan(plan, selectedRecurrency));

            return this;
        }

        public Subscription SetStatus(SubscriptionStage stage)
        {
            switch (Detail.Condition)
            {
                case SubscriptionStage.Trial:
                case SubscriptionStage.Inactive:
                    Detail.Condition = stage;
                    return this;

                case SubscriptionStage.Active:
                    Detail.Condition = SubscriptionStage.Inactive;
                    return this;

                default:
                    return this;
            }
        }

        public Subscription UpsertTrial(int trialDays, DateTime? trialStart = null)
        {
            trialStart ??= DateTime.Now;

            if (IsValidTrial(trialDays)) return this;

            Detail.TrialStart = DateOnly.FromDateTime((DateTime)trialStart);
            Detail.TrialEnd = Detail.TrialStart.AddDays(trialDays);

            SetStatus(SubscriptionStage.Trial);

            return this;
        }

        private bool IsValidTrial(int trialDays) => trialDays < 1 || Detail.Condition == SubscriptionStage.Active || Detail.TrialStart != DateOnly.MinValue;
    }
}