
using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserByID(string id);
    }
}
