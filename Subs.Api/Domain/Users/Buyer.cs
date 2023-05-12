using Subs.Api.Domain.Base;
using Subs.Api.Domain.Billing;
using Subs.Api.Domain.Products;

namespace Subs.Api.Domain.Users
{
    public class Buyer : BaseEntity<int>, IAggregateRoot
    {
        public string Email { get; set; } = default!;
        public ICollection<Payment> Payments { get; set; } = default!;
        public Subscription Subscription { get; set; } = null!;
    }
}