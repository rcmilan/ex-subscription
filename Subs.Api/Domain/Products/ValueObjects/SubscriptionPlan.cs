using Subs.Api.Domain.Enums;

namespace Subs.Api.Domain.Products.ValueObjects
{
    public class SubscriptionPlan
    {
        public SubscriptionPlan(Plan plan, PlanRecurrency recurrency) : this()
        {
            CreatedAt = DateTime.Now;

            Plan = plan;
            ValueInCents = recurrency.ValueInCents;
            Period = recurrency.Period;
        }

        public SubscriptionPlan()
        {
        }

        public DateTime CreatedAt { get; init; }

        public RecurrencePeriod Period { get; init; }

        public virtual Plan Plan { get; init; } = default!;

        public int ValueInCents { get; init; }

        public static SubscriptionPlan? GetCurrent(IEnumerable<SubscriptionPlan> plans)
            => plans?.OrderByDescending(p => p.CreatedAt)?.FirstOrDefault();
    }
}