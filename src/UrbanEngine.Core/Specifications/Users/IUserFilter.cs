using System;
using System.Collections.Generic;
using System.Text;
using UrbanEngine.SharedKernel.Paging;

namespace UrbanEngine.Core.Specifications.Users
{
	public interface IUserFilter : IPagingParameters
	{
		string AuthZeroId { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		string Email { get; set; }
		string CountryOrRegion { get; }
		string PostalCode { get; }
		bool? IsDeleted { get; }
	}
}
