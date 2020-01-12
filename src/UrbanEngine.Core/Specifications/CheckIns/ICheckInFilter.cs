using System;
using UrbanEngine.SharedKernel.Paging;

namespace UrbanEngine.Core.Specifications.CheckIns
{
    public interface ICheckInFilter : IPagingParameters
    {
        long? EventId { get; set; }
        long? UserId { get; set; }
        DateTimeOffset? CheckedInAt { get; set; }
    }
}
