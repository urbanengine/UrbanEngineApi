using Ardalis.SmartEnum;

namespace UrbanEngine.Core.Application.Entities.ScheduleAggregate
{
    public class EventType : SmartEnum<EventType, int>
    { 
        public static EventType Workshop = new EventType("Workshop", 1);
        public static EventType SpeakerSeries = new EventType("Speaker Series", 2);
        public static EventType FundRaiser = new EventType("Fund Raiser", 3);
        public static EventType Hackathon = new EventType("Hackathon", 4);
        public static EventType TownHall = new EventType("Town Hall", 5);

        protected EventType(string name, int value)
            : base(name, value) { } 
    }
}
