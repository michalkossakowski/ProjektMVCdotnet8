namespace ProjektMVCdotnet8.Entities
{
    public class FollowUserEntity
    {
        public int Id { get; set; }
        public int? FollowingUserId { get; set; }
        public int? FollowedUserId { get; set; }
        public UserEntity FollowingUser { get; set; }
        public UserEntity FollowedUser { get; set; }
    }
}
