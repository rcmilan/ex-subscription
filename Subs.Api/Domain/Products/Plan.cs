using Subs.Api.Domain.Base;
using Subs.Api.Domain.Enums;
using Subs.Api.Domain.Products.ValueObjects;

namespace Subs.Api.Domain.Products
{
    public class Plan : BaseEntity<int>, IAggregateRoot
    {
        public virtual IReadOnlyList<PlanRecurrency> Recurrencies { get; init; } = default!;
        public string Title { get; init; } = default!;

        public PlanRecurrency GetRecurrency(RecurrencePeriod period) => Recurrencies.Single(r => r.Period == period);
    }
}