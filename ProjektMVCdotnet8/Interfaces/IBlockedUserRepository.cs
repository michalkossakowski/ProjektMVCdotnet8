using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IBlockedUserRepository
    {
        Task<IEnumerable<BlockedUserEntity>> GetAllBlockedBy(string userSingedID);
        Task<IEnumerable<string>> GetAllIDBlockedBy(string userSingedID);
        bool Add(BlockedUserEntity post);
        bool Delete(BlockedUserEntity post);
        bool Delete(int id);
        bool Update(BlockedUserEntity post);
        bool Save();
    }
}
