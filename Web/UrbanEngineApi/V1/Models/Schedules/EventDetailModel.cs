using System.Collections.Generic;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Common.Validation;

namespace UrbanEngine.Web.UrbanEngineApi.Schedules
{
    public class EventDetailModel : IValidate
    {
        public Event AsEventToSchedule()
        {
            return new Event { };
        }

        public bool IsValid => string.IsNullOrEmpty(GetErrorMessage());

        public string GetErrorMessage()
        {
            return null;
        }

    }
}