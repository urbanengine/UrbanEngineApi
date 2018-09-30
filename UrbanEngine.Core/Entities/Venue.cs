using System.Collections.Generic;

namespace UrbanEngine.Core.Entities {
    /// <summary>
    /// a venue in which an event may be held at 
    /// </summary>
    public class Venue {
        #region Properties 

        /// <summary>
        /// uniquely identifies the venue 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name of the venue 
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// any events associated with venue 
        /// </summary>
        public virtual ICollection<Event> Events { get; set; }

        #endregion
    }
}
