using UrbanEngine.Core.Common.Results;

namespace UrbanEngine.Core.Application.Schedules
{
    public class ScheduleResult<TEntity> : CommandResult where TEntity : class
    {
        public ScheduleResult(TEntity scheduledItem, string message, bool success) 
            : base(message, success)
        {
            ScheduledItem = scheduledItem;
        }

        public TEntity ScheduledItem { get; private set; }
         
    }
}
