﻿using System.Collections.Generic;
using MediatR;
using UrbanEngine.Core.Models.Users;
using UrbanEngine.Core.Specifications.Users;
using UrbanEngine.SharedKernel.Paging;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Users
{
	public class GetUsersMessage : PagingParameters, IRequest<QueryResult<IEnumerable<UserListItemDto>>>, IUserFilter
	{
		public long? CompanyId { get; set; }
		public string CountryOrRegion { get; set; }
		public string PostalCode { get; set; }
		public bool? IsDeleted { get; set; }
	}
}