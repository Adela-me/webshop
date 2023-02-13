using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Product : Entity<ProductId>
{
    public Product() : base() { }

    public string Name { get; private set; }
    public string Description { get; private set; }
}
