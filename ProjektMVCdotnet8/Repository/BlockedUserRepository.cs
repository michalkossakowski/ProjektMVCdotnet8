using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;

namespace ProjektMVCdotnet8.Repository
{
    public class BlockedUserRepository : IBlockedUserRepository
    {
        private readonly ApplicationDbContext _context;


        public BlockedUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(BlockedUserEntity blockedUser)
        {
            _context.Add(blockedUser);
            return Save();
        }

        public bool Delete(BlockedUserEntity blockedUser)
        {
            _context.Remove(blockedUser);
            return Save();
        }

        public bool Delete(int id)
        {
            var blockedUser = _context.BlockedUsers.Where(b => b.Id.Equals(id));
            _context.Remove(blockedUser);
            return Save();
        }
        public async Task<IEnumerable<BlockedUserEntity>> GetAllBlockedBy(string userSingedID)
        {
            var blockedUsers = _context.BlockedUsers
                .Where(entry => entry.BlockingUser.Id == userSingedID)
                .ToListAsync();
            return await blockedUsers;
        }

        public async Task<IEnumerable<string>> GetAllIDBlockedBy(string userSingedID)
        {
            var blockedUser = await GetAllBlockedBy(userSingedID);
            var blockedUserID = blockedUser.Select(b => b.BlockedUser.Id);
            return blockedUserID;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
