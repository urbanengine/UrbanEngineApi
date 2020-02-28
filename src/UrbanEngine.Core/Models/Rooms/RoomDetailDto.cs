namespace UrbanEngine.Core.Models.Rooms
{
	public class RoomDetailDto
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int? Capacity { get; set; }
		public string Resources { get; set; }
		public long? VenueId { get; set; }
		public bool? HasTVOrProjector { get; set; }
	}
}
