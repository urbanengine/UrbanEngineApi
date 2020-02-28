using System.Collections.Generic;
using UrbanEngine.SharedKernel.Data;

namespace UrbanEngine.Core.Entities
{
	public class RoomEntity : EntityBase
	{
		#region Properties

		public string Name { get; private set; }
		public string Description { get; private set; }
		public int? Capacity { get; private set; }
		public string Resources { get; private set; }
		public long? VenueId { get; private set; }
		public bool? HasTVOrProjector { get; private set; }

		#endregion

        #region Navigation Properties

        public EventVenueEntity Venue { get; private set; }

		public IList<EventEntity> Events { get; private set; }

		#endregion

		#region Constructors

		protected RoomEntity() { }

		public RoomEntity(string name, string description, int? capacity, string resources, long venueId, bool hasTvOrProjector)
		{
			Name = name;
			Description = description;
			Capacity = capacity;
			Resources = resources;
			VenueId = venueId;
			HasTVOrProjector = hasTvOrProjector;
		}

		public RoomEntity(long id, string name, string description, int? capacity, string resources, long venueId, bool hasTvOrProjector)
			: this(name, description, capacity, resources, venueId, hasTvOrProjector)
		{
			Id = id;
		}

		#endregion
	}
}
