using System;
using System.Collections.Generic;
using System.Text;
using UrbanEngine.SharedKernel.Paging;

namespace UrbanEngine.Core.Specifications.Users
{
	public interface IUserFilter : IPagingParameters
	{
		long? CompanyId { get; }
		string CountryOrRegion { get; }
		string PostalCode { get; }
		bool? IsDeleted { get; }
	}
}
