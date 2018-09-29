using System;

namespace UrbanEngine.Core.Entities {
    /// <summary>
    /// an event that is taking place 
    /// </summary>
    public class Event {
        /// <summary>
        /// uniquely identifies the event
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name of the event 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// description of the vent 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// timestamp of when event begins 
        /// </summary>
        public DateTime? StartTimestamp { get; set; }

        /// <summary>
        /// timestamp of when event ends 
        /// </summary>
        public DateTime? EndTimestamp { get; set; }

        /// <summary>
        /// id of venue where event is held 
        /// </summary>
        public long VenueId { get; set; }

        /// <summary>
        /// details about the venue where the vent is held
        /// </summary>
        public Venue Venue { get; set; }
    }
}
