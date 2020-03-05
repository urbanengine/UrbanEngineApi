using UrbanEngine.SharedKernel.Data;

namespace UrbanEngine.Core.Entities
{
	public class UserEntity : EntityBase
	{
		#region Properties

		/// <summary>
		/// identifier associated with Auth0 account
		/// </summary>
		public string AuthZeroId { get; set; }

		/// <summary>
		/// given name (first name) of the user
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// family name (last name) of the user
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// email address of the user
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// country or region
		/// </summary>
		public string CountryOrRegion { get; set; }

		/// <summary>
		/// postal code
		/// </summary>
		public string PostalCode { get; set; }

		#endregion
	}
}
