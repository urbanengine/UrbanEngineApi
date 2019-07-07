using UrbanEngine.Core.Common.Results;

namespace UrbanEngine.Core.Application.Schedules
{
    public class ScheduleResult<TEntity> : CommandResult where TEntity : class
    {
        public ScheduleResult(TEntity scheduledItem, string message, int? statusCode = null, bool? success = null) 
            : base(message, statusCode, success)
        {
            ScheduledItem = scheduledItem;
        }

        public TEntity ScheduledItem { get; private set; }
         
    }
}
