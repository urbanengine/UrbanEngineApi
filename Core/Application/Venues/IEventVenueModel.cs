using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Common.Validation;

namespace UrbanEngine.Core.Application.Venues
{
    public interface IEventVenueModel
    {
        Expression<Func<EventVenue, IEventVenueModel>> Projection { get; }

        EventVenue ToDomainEntity();

        IEventVenueModel FromDomainEntity(EventVenue eventVenue);
    }
}
