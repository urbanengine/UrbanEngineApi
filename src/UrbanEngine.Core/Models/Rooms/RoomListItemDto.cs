namespace UrbanEngine.Core.Models.Rooms
{
	public class RoomListItemDto
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public int? Capacity { get; set; }
		public long? VenueId { get; set; }
	}
}
