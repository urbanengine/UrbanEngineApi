using System.Collections.Generic;
using UrbanEngine.Core.Application.SharedKernel;

namespace UrbanEngine.Core.Application.Entities.ScheduleAggregate
{
    public class EventVenue : Entity<long>
    {
        #region Properties

        public string Name { get; private set; }
         
        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public RegionType Region { get; set; }

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
        
        public EventVenue(long id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion
    }
}
