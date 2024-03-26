using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IBlockedUserRepository
    {
        Task<IEnumerable<BlockedUserEntity>> GetAll();
        Task<BlockedUserEntity> GetByIdAsync(int id);
        bool Add(BlockedUserEntity post);
        bool Delete(BlockedUserEntity post);
        bool Delete(int id);
        bool Update(BlockedUserEntity post);
        bool Save();
    }
}
