namespace Domain.Primitives;
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{

    private readonly List<IDomainEvent> domainEvents = new();

    public Entity()
    {

    }

    protected Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; protected set; }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => domainEvents.ToList();
    public void ClearDomainEvents() => domainEvents.Clear();
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
