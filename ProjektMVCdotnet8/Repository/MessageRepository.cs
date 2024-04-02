using Microsoft.EntityFrameworkCore;
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
            _context = context;
        }
        public bool Add(MessageEntity report)
        {
            _context.Add(report);
            return Save();
        }
        public async Task<IEnumerable<MessageEntity>> GetAll()
        {
            return await _context.Messages.ToListAsync();
        }
        public async Task<MessageEntity> GetById(int id)
        {
            return await _context.Messages.FindAsync(id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
