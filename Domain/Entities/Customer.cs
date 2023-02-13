using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Customer : Entity<CustomerId>
{
    public string UserId { get; set; }
    public Address Address { get; set; }
}
