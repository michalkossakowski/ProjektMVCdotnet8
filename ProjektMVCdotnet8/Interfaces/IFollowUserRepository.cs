using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
namespace ProjektMVCdotnet8.Interfaces
{
    public interface IFollowUserRepository
    {
        Task<IEnumerable<FollowUserEntity>> GetAll();
        Task<IEnumerable<UserEntity>> GetAllFollowed();
        Task<IEnumerable<UserEntity>> GetAllFollowedBY(string id);

        Task<FollowUserEntity> GetById(string followedUser, string userSignedID);

        bool Add(FollowUserEntity post);
        bool Delete(FollowUserEntity post);
        bool Delete(int id);
        bool Update(FollowUserEntity post);
        bool Save();
    }
}
