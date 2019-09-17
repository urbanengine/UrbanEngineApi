using System;
using UrbanEngine.Core.Application.SharedKernel;

namespace UrbanEngine.Tests.Persistence.Tests.TestHelpers
{
    public class FakeEntity : IEntity<long>
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        public string Name { get; set; }

        public string Value { get; set; }

        public FakeEntity(long id, string name, string value)
        {
            Id = id;
            Name = name;
            Value = value;
        }
    }
}
