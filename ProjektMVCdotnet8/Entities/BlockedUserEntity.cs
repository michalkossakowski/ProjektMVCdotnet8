namespace ProjektMVCdotnet8.Entities
{
    public class BlockedUserEntity
    {
        public int Id { get; set; }
        public int? BlockedUserId { get; set; }
        public int? BlockingUserId { get; set; }
        public UserEntity BlockingUser { get; set; }
        public UserEntity BlockedUser { get; set; }
    }
}
