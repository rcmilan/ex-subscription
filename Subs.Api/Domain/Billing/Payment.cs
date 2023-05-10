using Subs.Api.Domain.Base;

namespace Subs.Api.Domain.Billing
{
    public class Payment : BaseEntity<Guid>
    {
        public Payment()
        {
        }

        public Payment(int valueInCents, DateOnly expiresAt) : this()
        {
            CratedAt = DateTime.Now;

            ValueInCents = valueInCents;
            ExpiresAt = expiresAt;
        }

        public DateTime CratedAt { get; init; }
        public DateOnly ExpiresAt { get; init; }
        public bool IsExpired => !DateOnValidRange(DateTime.Now);
        public bool IsPaid => PaidAt > DateTime.MinValue && DateOnValidRange(PaidAt);
        public DateTime PaidAt { get; set; } = DateTime.MinValue;
        public int ValueInCents { get; init; }

        public Payment Pay()
        {
            if (IsExpired || IsPaid) return this;

            PaidAt = DateTime.Now;

            return this;
        }

        private bool DateOnValidRange(DateTime date)
            => date.Year < ExpiresAt.Year ||
            date.Month < ExpiresAt.Month ||
            date.Day <= ExpiresAt.Day;
    }
}