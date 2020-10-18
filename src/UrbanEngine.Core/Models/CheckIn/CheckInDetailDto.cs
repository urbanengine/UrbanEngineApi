using System;

namespace UrbanEngine.Core.Models.CheckIn
{
	public class CheckInDetailDto
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset CheckedInAt { get; set; }
        public long UserId { get; set; }
        public long? EventId { get; set; }
    }
}
