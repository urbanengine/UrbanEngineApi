using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Common.Validation;

namespace UrbanEngine.Services.UrbanEngineApi.Schedules
{
    public class SessionDetailModel : IValidate
    {

        public EventSession AsSessionToSchedule()
        {
            return new EventSession { };
        }

        public bool IsValid => string.IsNullOrEmpty(GetErrorMessage());

        public string GetErrorMessage()
        {
            return null;
        }

    }
}