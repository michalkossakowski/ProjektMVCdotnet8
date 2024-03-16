using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Entities
{
    public class BlockedUserEntity
    {
        public int Id { get; set; }
        public UserEntity BlockingUser { get; set; }
        public UserEntity BlockedUser { get; set; }
    }
}
