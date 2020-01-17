using System;
using UrbanEngine.Core.Entities;

namespace UrbanEngine.Core.Models.CheckIn {
    public class CheckInDetailDto
    {
        #region Properties
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTimeOffset CheckedInAt { get; set; }
        public long UserId { get; set; }
        public long? EventId { get; set; }

        #endregion

        #region Navigation Properties

        public CheckInEntity Event { get; set; }

        #endregion
    }
}
