using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IChatRepository
    {
        Task<IEnumerable<ChatEntity>> GetAll();
        Task<ChatEntity> GetById(int id);
        bool Add(ChatEntity contact);
        bool Save();
    }
}
