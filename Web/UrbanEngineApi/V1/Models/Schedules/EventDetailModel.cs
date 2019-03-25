using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.SharedKernel;
using UrbanEngine.Core.Common.Validation;

namespace UrbanEngine.Web.UrbanEngineApi.Schedules
{
    /// <summary>
    /// represents details specified by end user to manage events
    /// </summary>
    public class EventDetailModel : IValidate
    {
        #region Properties  
        /// <summary>
        /// date range for the event 
        /// </summary>
        public DateTimeRange Duration { get; set; }
        /// <summary>
        /// display name of event 
        /// </summary>
        public string Title { get; set; } 
        /// <summary>
        /// event description that provides potential attendees details on what 
        /// to expect at the event 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// type of event, must correspond to an EventType
        /// </summary>
        public int EventTypeValue { get; set; }
        /// <summary>
        /// identifies the person organizing the event
        /// </summary>
        public string OrganizerId { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// converts ViewModel to Domain Entity
        /// converts ViewModel to Domain Entity
        /// </summary>
        /// <returns></returns>
        public static Event ToDomainEntity(EventDetailModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return new Event(
                model.Title, 
                model.Description, 
                EventType.FromValue(model.EventTypeValue), 
                model.Duration.Start, 
                model.Duration.End, 
                model.OrganizerId);
        }

        /// <summary>
        /// convert a domain entity to a view model
        /// </summary>
        /// <param name="eventDetail"></param>
        /// <returns></returns>
        public static EventDetailModel FromDomainEntity(Event eventDetail)
        {
            if (eventDetail == null)
                throw new ArgumentNullException(nameof(eventDetail));

            return new EventDetailModel
            {
                Description = eventDetail.Description,
                Duration = new DateTimeRange(eventDetail.StartDate.Value, eventDetail.EndDate.Value),
                EventTypeValue = eventDetail.EventType.Value,
                OrganizerId = eventDetail.OrganizerId,
                Title = eventDetail.Title
            };
        }

        #endregion

        #region IValidate 

        /// <summary>
        /// true if there are no validation errors for properties
        /// </summary>
        [JsonIgnore]
        public bool IsValid => string.IsNullOrEmpty(GetErrorMessage());

        /// <summary>
        /// returns error message if any validation errors exist
        /// </summary>
        /// <returns></returns>
        public string GetErrorMessage()
        {
            IList<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Title))
                errors.Add($"{nameof(Title)} is a required value can cannot be null.");

            if (!EventType.TryFromValue(EventTypeValue, out var eventTypeEnum))
                errors.Add($"{nameof(EventTypeValue)} must be a valid {nameof(EventType)}");

            if (Duration == null)
                errors.Add($"{nameof(Duration)} cannot be null");

            return string.Join(" ", errors);
        }

        #endregion
    }
}