namespace UrbanEngine.Core.Entities {
    /// <summary>
    /// a venue in which an event may be held at 
    /// </summary>
    public class Venue {
        /// <summary>
        /// uniquely identifies the venue 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name of the venue 
        /// </summary>
        public string Name { get; set; }
    }
}
