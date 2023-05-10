using Subs.Api.Domain.Base;
using Subs.Api.Domain.Finance;

namespace Subs.Api.Domain.Users
{
    public class Buyer : BaseEntity<int>, IAggregateRoot
    {
        public string Email { get; set; } = default!;
        public IList<Payment> Payments { get; set; } = default!;
    }
}