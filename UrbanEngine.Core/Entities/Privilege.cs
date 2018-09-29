using System.Collections.Generic;

namespace UrbanEngine.Core.Entities {
    /// <summary>
    /// identifies a privilege in the system 
    /// </summary>
    public class Privilege {
        /// <summary>
        /// uniquely identifies a privilege 
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// name of the privilege 
        /// </summary>
        public string Name { get; set; } 
    }
}
