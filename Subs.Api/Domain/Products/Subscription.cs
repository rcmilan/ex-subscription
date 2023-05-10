using Subs.Api.Domain.Base;
using Subs.Api.Domain.Products.ValueObjects;
using static Subs.Api.Domain.Products.ValueObjects.SubscriptionPlan;

namespace Subs.Api.Domain.Products
{
    public class Subscription : BaseEntity<Guid>, IAggregateRoot
    {
        public DateTime CreatedAt { get; init; } = default!;

        public Plan CurrentPlan => GetCurrent(PlanHistory);
        public virtual IList<SubscriptionPlan> PlanHistory { get; set; } = default!;

        public Subscription Add(Plan plan, PlanRecurrency selectedRecurrency)
        {
            if (!plan.Recurrencies.Contains(selectedRecurrency)) return this;

            PlanHistory ??= new List<SubscriptionPlan>();

            PlanHistory.Add(new SubscriptionPlan(plan, selectedRecurrency));

            return this;
        }
    }
}