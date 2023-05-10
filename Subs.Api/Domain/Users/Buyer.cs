using Subs.Api.Domain.Base;

namespace Subs.Api.Domain.Users
{
    public class Buyer : BaseEntity<int>, IAggregateRoot
    {
        public string Email { get; set; } = default!;
    }
}