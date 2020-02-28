using UrbanEngine.SharedKernel.Paging;

namespace UrbanEngine.Core.Specifications.Rooms
{
	public interface IRoomFilter : IPagingParameters
	{
		string Name { get; }
		int? MinCapacity { get; }
		long? VenueId { get; }
		bool? HasTvOrProjector { get; }
		string Resources { get; }
        bool? IsDeleted { get; }
	}
}
