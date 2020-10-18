using MediatR;
using System;
using System.Collections.Generic;
using UrbanEngine.Core.Models.CheckIn;
using UrbanEngine.Core.Specifications.CheckIns;
using UrbanEngine.SharedKernel.Paging;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.CheckIn
{
    public class GetCheckInsMessage : PagingParameters, IRequest<QueryResult<IEnumerable<CheckInListItemDto>>>, ICheckInFilter, IPagingParameters
    {
        public long? EventId { get; set; }
        public long? UserId { get; set; }
        public DateTimeOffset? CheckedInAt { get; set; }
		public bool? IsDeleted { get; set; }
		public long? CheckInId { get; set; }
	}
}