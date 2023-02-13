using Domain.Primitives;

namespace Domain.ValueObjects;
public sealed class CustomerId : ValueObject
{
    private CustomerId(Guid guid)
    {
        Value = guid;
    }

    public Guid Value { get; }

    public static CustomerId CreateUnique()
    {
        return new CustomerId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
