using PrimatScheduleBot.Behavior.Interfaces;
using System;
using System.Threading.Tasks;

namespace PrimatScheduleBot.Behavior;

internal class It<TObject> where TObject : IEquatable<TObject>
{
    private readonly TObject _item;

    public It(TObject item)
    {
        _item = item;
    }

    public ICondition Is(TObject other)
    {
        return new BoolCondition(() => _item.Equals(other));
    }

    public ICondition IsNot(TObject other)
    {
        return new BoolCondition(() => _item.Equals(other) == false);
    }

    private class BoolCondition(Func<bool> _predicate) : ICondition
    {
        public Task<bool> IsTrueAsync()
        {
            return Task.FromResult(_predicate.Invoke());
        }
    }
}
