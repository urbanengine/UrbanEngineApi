using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Events;
using UrbanEngine.Core.Common.Validation;

namespace UrbanEngine.Services.UrbanEngineApi.V1.Models.Events
{
    public class EventDetailModel : IEventModel, IValidate
    {
        
        #region Properties 
         
        public  long Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Type { get; set; }
        public string OrganizerId { get; private set; } 
        public long? VenueId { get; set; }

        #endregion

        #region IEventModel Members
        
        [JsonIgnore]
        public Expression<Func<Event, IEventModel>> Projection => x => FromDomainEntity(x);

        public Event ToDomainEntity(long? id = null)
        {
            if (!IsValid)
                throw new Exception(GetErrorMessage());

            return new Event(
                title: Title,
                description: Description,
                eventType: !string.IsNullOrEmpty(Type) ? EventType.FromName(Type) : null,
                startDate: StartDate,
                endDate: EndDate,
                organizerId: OrganizerId,
                venueId: VenueId);
        }

        public IEventModel FromDomainEntity(Event eventData)
        {
            return new EventDetailModel
            {
                Id = eventData.Id,
                Title = eventData.Title,
                Description = eventData.Description,
                Type = eventData.EventType?.Name,
                StartDate = eventData.StartDate,
                EndDate = eventData.EndDate,
                OrganizerId = eventData.OrganizerId,
                VenueId = eventData.VenueId
            };
        }

        #endregion
        
        #region IValidate Members

        /// <summary>
        /// true if there are no validation errors for properties
        /// </summary>
        [JsonIgnore]
        public bool IsValid => string.IsNullOrEmpty(GetErrorMessage());

        public string GetErrorMessage()
        {
            IList<string> errors = new List<string>();

            if(string.IsNullOrEmpty(Title))
                errors.Add($"{nameof(Title)} is a required value and cannot be null");
            
            if(string.IsNullOrEmpty(Description))
                errors.Add($"{nameof(Description)} is a required value and cannot be null");
            
            if(!StartDate.HasValue)
                errors.Add($"{nameof(StartDate)} is a required value and cannot be null");

            if(string.IsNullOrEmpty(OrganizerId))
                errors.Add($"{nameof(OrganizerId)} is a required value and cannot be null");

            if(!VenueId.HasValue)
                errors.Add($"{nameof(VenueId)} is a required value and cannot be null");

            return string.Join(" ", errors);
        }
        
        #endregion
    }
}
