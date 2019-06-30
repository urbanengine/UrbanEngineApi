using System.Collections.Generic;

namespace UrbanEngine.Core.Entities {
    /// <summary>
    /// identifies a tag in the system 
    /// </summary>
    public class Tag {
        #region Properties 

        /// <summary>
        /// uniquely identifies a tag 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name of the tag 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// type of tag 
        /// </summary>
        public string Type { get; set; }

        #endregion

        #region Navigation Properties 

        /// <summary>
        /// users with this tag 
        /// </summary>
        public virtual ICollection<TaggedUser> TaggedUsers { get; set; }

        #endregion
    }
}
