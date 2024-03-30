using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<MessageEntity>> GetAll();
        Task<MessageEntity> GetById(int id);
        bool Add(MessageEntity contact);
        bool Save();
    }
}
