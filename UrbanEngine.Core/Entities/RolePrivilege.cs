namespace UrbanEngine.Core.Entities {
    public class RolePrivilege {
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
        public long PrivilegeId { get; set; }
        public virtual Privilege Privilege { get; set; }
    }
}
