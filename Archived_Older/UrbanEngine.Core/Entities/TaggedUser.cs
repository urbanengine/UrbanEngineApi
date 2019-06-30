namespace UrbanEngine.Core.Entities {
    public class TaggedUser {
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
