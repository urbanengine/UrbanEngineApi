using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;

namespace UrbanEngine.Core.Application.Venues
{
    public interface IEventVenueModel
    {
        Expression<Func<EventVenue, IEventVenueModel>> Projection { get; }

        EventVenue ToDomainEntity(long? id = null);

        IEventVenueModel FromDomainEntity(EventVenue eventVenue); 
    }
}
