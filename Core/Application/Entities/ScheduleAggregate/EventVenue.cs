using System.Collections.Generic;
using UrbanEngine.Core.Application.SharedKernel;

namespace UrbanEngine.Core.Application.Entities.ScheduleAggregate
{
    public class EventVenue : Entity<long>
    {
        #region Properties

        public string Name { get; private set; }

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

        #endregion
    }
}
