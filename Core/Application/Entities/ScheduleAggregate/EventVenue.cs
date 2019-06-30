using System.Collections.Generic;
using UrbanEngine.Core.Application.SharedKernel;

namespace UrbanEngine.Core.Application.Entities.ScheduleAggregate
{
    public class EventVenue : Entity<long>
    {
        #region Properties

        public string Name { get; private set; }

        public Location Location { get; private set; }

        #endregion

        #region Navigation Properties

        public IList<Event> Events { get; private set; }

        #endregion

        #region Constructors

        protected EventVenue() { }

        public EventVenue(string name)
        {
            Name = name;
        }

        public EventVenue(string name, Location location)
        {
            Name = name;
            Location = location;
        }

        public EventVenue(long id, string name, Location location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        #endregion
    }
}
