using System.Collections.Generic;

namespace UrbanEngine.Core.Entities {
    /// <summary>
    /// identifies a privilege in the system 
    /// </summary>
    public class Privilege {
        #region Properties 

        /// <summary>
        /// uniquely identifies a privilege 
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// name of the privilege 
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Navigation Properties 

        /// <summary>
        /// any roles assigned to this privilege 
        /// </summary>
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }

        #endregion
    }
}
