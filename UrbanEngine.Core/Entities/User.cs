using System;

namespace UrbanEngine.Core
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsElite { get; set; }
        public string AuthZeroId { get; set; }
    }
}
