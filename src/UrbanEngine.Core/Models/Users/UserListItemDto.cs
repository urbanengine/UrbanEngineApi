namespace UrbanEngine.Core.Models.Users
{
	public class UserListItemDto
	{
		/// <summary>
		/// id of the user
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// id that identifies company that the user works for or is associated with  
		/// </summary>
		public long? CompanyId { get; set; }

		/// <summary>
		/// country or region 
		/// </summary>
		public string CountryOrRegion { get; set; }

		/// <summary>
		/// postal code 
		/// </summary>
		public string PostalCode { get; set; }
	}
}
