using System;

namespace UrbanEngine.Core.Models.CheckIn {
    public class CheckInListItemDto
    {
        #region Properties

        public DateTimeOffset CheckedInAt { get; set; }
        public long UserId { get; set; }
        public long? EventId { get; set; }

        #endregion
    }
}
