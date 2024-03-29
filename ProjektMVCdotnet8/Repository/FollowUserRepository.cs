using Microsoft.AspNetCore.Identity;
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
            _context.Remove(post);
            return Save();
        }

        public async Task<IEnumerable<FollowUserEntity>> GetAll()
        {
            var followUsers = _context.FollowUsers
                .Include(f => f.FollowingUser)
                .Include(f => f.FollowedUser)
                .ToListAsync();
            return await followUsers;
        }

        public async Task<IEnumerable<UserEntity>> GetAllFollowedBY(string id)
        {
            var allFollow = await GetAll();
            var followedUsers = allFollow
                .Where(f => f.FollowingUser.Id == id)
                .Select(f => f.FollowedUser);
            return followedUsers;
        }

        public async Task<FollowUserEntity> GetById(string followedUser, string userSignedID)
        {
            var allFollow = await GetAll();
            var getFolloUser = allFollow
                .Where(user => user.FollowedUser.Id.Equals(followedUser) && user.FollowingUser.Id.Equals(userSignedID))
                .FirstOrDefault();
            return getFolloUser;

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
