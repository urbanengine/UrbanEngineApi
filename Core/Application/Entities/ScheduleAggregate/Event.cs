using UrbanEngine.Core.Application.SharedKernel;

namespace UrbanEngine.Core.Application.Entities.ScheduleAggregate
{
    public class Event : Entity<long>
    { 
        public string Name { get; private set; }
        public string Description { get; private set; }

        protected Event()
        {

        }

        public Event(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
