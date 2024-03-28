using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<MessageEntity>> GetAll();
        Task<MessageEntity> GetByIdAsync(int id);
        bool Add(MessageEntity message);
        bool Delete(MessageEntity message);
        bool Delete(int id);
        bool Update(MessageEntity message);
        bool Save();
    }
}
