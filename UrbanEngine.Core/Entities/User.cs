using System.Collections.Generic;

namespace UrbanEngine.Core.Entities {
    /// <summary>
    /// a user that interacts with the system
    /// </summary>
    public class User {
        /// <summary>
        /// uniquely identifies a user
        /// </summary>
        public long Id { get; set; } 

        /// <summary>
        /// identifier associated with Auth0 account 
        /// </summary>
        public string AuthZeroId { get; set; }

        /// <summary>
        /// user bio
        /// </summary>
        public string Bio { get; set; }

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

        /// <summary>
        /// details about the company a user works for or is associated with 
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// roles assigned to the user 
        /// </summary>
        public IEnumerable<UserRole> Roles { get; set; }

        /// <summary>
        /// tags assigned to the user 
        /// </summary>
        public IEnumerable<TaggedUser> Tags { get; set; }
    }
}
