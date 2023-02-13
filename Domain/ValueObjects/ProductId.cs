using Domain.Primitives;

namespace Domain.ValueObjects;
public sealed class ProductId : ValueObject
{
    private ProductId(Guid guid)
    {
        Value = guid;
    }

    public Guid Value { get; }

    public static ProductId CreateUnique()
    {
        return new ProductId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
