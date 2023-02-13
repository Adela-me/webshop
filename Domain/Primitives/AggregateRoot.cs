namespace Domain.Primitives;
public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    public AggregateRoot()
    {

    }

    protected AggregateRoot(TId id) : base(id)
    {
    }
}
