using Subs.Api.Domain.Enums;

namespace Subs.Api.Domain.Products.ValueObjects
{
    public class PlanRecurrency
    {
        public RecurrencePeriod Period { get; set; }

        public int ValueInCents { get; set; }
    }
}