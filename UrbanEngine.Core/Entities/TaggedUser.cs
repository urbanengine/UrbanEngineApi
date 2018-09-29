namespace UrbanEngine.Core.Entities {
    public class TaggedUser {
        public long UserId { get; set; }
        public User User { get; set; }
        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
