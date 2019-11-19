using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;

namespace UrbanEngine.Core.Application.Events
{
    public interface IEventModel
    {
        Expression<Func<Event, IEventModel>> Projection { get; }

        Event ToDomainEntity(long? id = null);

        IEventModel FromDomainEntity(Event eventData);
    }
}
