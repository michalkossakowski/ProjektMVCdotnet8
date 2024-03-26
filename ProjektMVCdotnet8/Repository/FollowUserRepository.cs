using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;

namespace ProjektMVCdotnet8.Repository
{
    public class FollowUserRepository : IFollowUserRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowUserRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool Add(FollowUserEntity post)
        {
            throw new NotImplementedException();
        }

        public bool Delete(FollowUserEntity post)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FollowUserEntity>> GetAll()
        {
            var followUsers = _context.FollowUsers
                .Include(f => f.FollowingUser)
                .Include(f => f.FollowedUser)
                .ToListAsync();
            return await followUsers;
        }

        public async Task<IEnumerable<UserEntity>> GetAllFollowed()
        {
            var allFollow = await GetAll();
            var followedUsers = _context.FollowUsers
                .Include(f => f.FollowedUser)
                .Select(f => f.FollowedUser)
                .ToListAsync();
            return await followedUsers;
        }

        public async Task<IEnumerable<UserEntity>> GetAllFollowed(string id)
        {
            var allFollow = await GetAll();
            var followedUsers = allFollow
                .Where(f => f.FollowingUser.Id == id)
                .Select(f => f.FollowedUser);
            return  followedUsers;
        }

        public Task<FollowUserEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(FollowUserEntity post)
        {
            throw new NotImplementedException();
        }
    }
}
