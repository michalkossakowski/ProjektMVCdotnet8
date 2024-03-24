using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Entities
{
    public class FollowUserEntity
    {
        public int Id { get; set; }
        public UserEntity FollowingUser { get; set; }
        public UserEntity FollowedUser { get; set; }
        public FollowUserEntity() { }
        public FollowUserEntity(UserEntity FollowingUser, UserEntity FollowedUser) 
        {
            this.FollowingUser = FollowingUser;
            this.FollowedUser = FollowedUser;
        }

    }

}
