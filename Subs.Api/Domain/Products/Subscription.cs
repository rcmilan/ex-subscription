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

        public virtual ICollection<SubscriptionPlan> PlanHistory { get; set; } = new List<SubscriptionPlan>();

        public Subscription Add(Plan plan, RecurrencePeriod selectedRecurrencyPeriod) => Add(plan, plan.GetRecurrency(selectedRecurrencyPeriod));

        public Subscription Add(Plan plan, PlanRecurrency selectedRecurrency)
        {
            if (!plan.Recurrencies.Contains(selectedRecurrency)) return this;

            PlanHistory.Add(new SubscriptionPlan(plan, selectedRecurrency));

            return this;
        }
    }
}