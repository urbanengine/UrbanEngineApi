using System;
using UrbanEngine.SharedKernel.Data;

namespace UrbanEngine.Core.Entities
{
    public class CheckInEntity : EntityBase
    {
        #region Properties

        public DateTimeOffset CheckedInAt { get; set; }
        public long UserId { get; set; }
        public long? EventId { get; set; }

        #endregion

        #region Navigation Properties

        public EventEntity Event { get; set; }

        #endregion
    }
}
