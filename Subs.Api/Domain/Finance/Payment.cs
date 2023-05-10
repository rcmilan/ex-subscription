using Subs.Api.Domain.Base;

namespace Subs.Api.Domain.Finance
{
    public class Payment : BaseEntity<Guid>
    {
        public Payment()
        {
        }

        public Payment(int valueInCents) : this()
        {
            CratedAt = DateTime.Now;

            ValueInCents = valueInCents;
        }

        public DateTime CratedAt { get; init; }
        public DateOnly ExpiresAt { get; init; }
        public bool IsPaid => (PaidAt > DateTime.MinValue) && DateOnValidRange(PaidAt);
        public DateTime PaidAt { get; set; } = DateTime.MinValue;
        public int ValueInCents { get; init; }

        public Payment Pay()
        {
            if (DateOnValidRange(DateTime.Now) || IsPaid) return this;

            PaidAt = DateTime.Now;

            return this;
        }

        private bool DateOnValidRange(DateTime date) => ExpiresAt.CompareTo(date) >= 0;
    }
}