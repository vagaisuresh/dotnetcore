namespace AuthUserIdentity.Models
{
    public class Userrolemaster
    {
        public short Id { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsRemoved { get; set; }
    }
}