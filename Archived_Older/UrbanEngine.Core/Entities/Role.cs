using System.Collections.Generic;

namespace UrbanEngine.Core.Entities {
    /// <summary>
    /// identifies a role a user may have in the system 
    /// </summary>
    public class Role {
        #region Properties 

        /// <summary>
        /// uniquely identifies a role 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name of the role 
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Navigation Properties 

        /// <summary>
        /// any privileges assigned to the role 
        /// </summary>
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }

        /// <summary>
        /// any users assigned to the role 
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}
