namespace UrbanEngine.Core {
    /// <summary>
    /// a user that interacts with the system
    /// </summary>
    public class User {
        /// <summary>
        /// uniquely identifies a user
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// first name or given name of the user 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// last name or surname of the user 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// whether or not the user account is considered an Elite account 
        /// </summary>
        public bool IsElite { get; set; }
        /// <summary>
        /// identifier associated with Auth0 account 
        /// </summary>
        public string AuthZeroId { get; set; }
    }
}
