using Microsoft.Extensions.Hosting;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;

namespace ProjektMVCdotnet8.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool Add(MessageEntity message)
        {
            _context.Add(message);
            return Save();
        }

        public bool Delete(MessageEntity message)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<MessageEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(MessageEntity message)
        {
            throw new NotImplementedException();
        }
    }
}
