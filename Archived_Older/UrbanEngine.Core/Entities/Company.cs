using System.Collections.Generic;

namespace UrbanEngine.Core.Entities {
    /// <summary>
    /// identifies information about a company 
    /// </summary>
    public class Company {
        #region Properties 

        /// <summary>
        /// uniquely identifies a company 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name of the company 
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Navigation Properties 

        /// <summary>
        /// users associated with the company 
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        #endregion
    }
}
