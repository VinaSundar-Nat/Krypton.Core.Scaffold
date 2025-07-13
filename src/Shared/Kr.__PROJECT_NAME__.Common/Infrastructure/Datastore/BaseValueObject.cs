using System;

namespace Kr.__PROJECT_NAME__.Common.Infrastructure.Datastore;

public abstract class BaseValueObject : IEquatable<BaseValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(BaseValueObject? obj)
    {
        return Equals(obj as object);
    }

    public override bool Equals(object? other)
    {
        if (GetType() != other?.GetType()) return false;

        if (other == null) return this == null;

        var _other = (BaseValueObject)other;

        return this.GetEqualityComponents().SequenceEqual(_other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        var hashVal = new Func<int, object, int>((hashCode, val) =>
        {
            var curHash = val?.GetHashCode() ?? 0;
            return hashCode * new Random().Next() + curHash;
        });

        unchecked
        {
            return GetEqualityComponents()
                    .Select(x => x?.GetHashCode() ?? 0)
                    .Aggregate((x, y) => (x * 17) ^ y);
        }
    }
}
