using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Venues;
using UrbanEngine.Core.Common.Validation;

namespace UrbanEngine.Services.UrbanEngineApi.V1.Models.Venues
{
    /// <summary>
    /// represents details about an event venue
    /// </summary>
    public class EventVenueDetailModel : IEventVenueModel, IValidate
    {
        public  long Id { get; private set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        #region IEventVenueModel Members

        [JsonIgnore]
        public Expression<Func<EventVenue, IEventVenueModel>> Projection => x => FromDomainEntity(x);

        public EventVenue ToDomainEntity(long? id = null)
        {
            if (!IsValid)
                throw new Exception(GetErrorMessage());

            return new EventVenue(id ?? 0, Name)
            { 
                Address = Address,
                Address2 = Address2,
                City = City,
                State = State, 
                PostalCode = PostalCode,
                Country = Country,
                Region = !string.IsNullOrEmpty(Region) ? RegionType.FromName(Region) : null
            };
        }

        public IEventVenueModel FromDomainEntity(EventVenue eventVenue)
        {
            return new EventVenueDetailModel
            {
                Id = eventVenue.Id,
                Name = eventVenue.Name,
                Address = eventVenue.Address,
                Address2 = eventVenue.Address2,
                City = eventVenue.City,
                State = eventVenue.State,
                PostalCode = eventVenue.PostalCode,
                Country = eventVenue.Country,
                Region = eventVenue.Region?.Name
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

            if (string.IsNullOrEmpty(Name))
                errors.Add($"{nameof(Name)} is a required value can cannot be null.");

            return string.Join(" ", errors);
        }
        
        #endregion
    }
}
