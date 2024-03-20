using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Entities
{
    public class BlockedUserEntity
    {
        public int Id { get; set; }
        public UserEntity BlockingUser { get; set; }
        public UserEntity BlockedUser { get; set; }
        public BlockedUserEntity() { }
        public BlockedUserEntity(UserEntity BlockingUser, UserEntity BlockedUser) 
        {
            this.BlockingUser = BlockingUser;
            this.BlockedUser = BlockedUser;
        }


    }
}
