using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;

namespace ProjektMVCdotnet8.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(ChatEntity report)
        {
            _context.Add(report);
            return Save();
        }
        public async Task<IEnumerable<ChatEntity>> GetAll()
        {
            return await _context.Chats.ToListAsync();
        }
        public async Task<ChatEntity> GetById(int id)
        {
            return await _context.Chats.FindAsync(id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
