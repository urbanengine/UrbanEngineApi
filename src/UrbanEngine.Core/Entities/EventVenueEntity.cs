using System.Collections.Generic;
using UrbanEngine.Core.Enums;
using UrbanEngine.SharedKernel.Data;

namespace UrbanEngine.Core.Entities
{
    public class EventVenueEntity : EntityBase
    {
        #region Properties

        public string Name { get; set; } // required

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
        
        public RegionType Region { get; set; }

        public bool IsAvailable { get; set; } // required

        #endregion

        #region Navigation Properties

        public IList<EventEntity> Events { get; private set; }

		public IList<RoomEntity> Rooms { get; private set; }

        #endregion

        #region Constructors

        protected EventVenueEntity() { }

        public EventVenueEntity(string name)
        {
            Name = name;
        }
        
        public EventVenueEntity(long id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion
    }
}
