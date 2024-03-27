using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;

namespace ProjektMVCdotnet8.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool Add(PostEntity post)
        {
            _context.Add(post);
            return Save();
        }
        public bool Delete(int id)
        {
            var post = GetByIdAsync(id);
            if (post is not null)
            {
                _context.Remove(post);
                return Save();
            }
            return false;
        }
        public bool Delete(PostEntity post)
        {
            _context.Remove(post);
            return Save();
        }

        public async Task<IEnumerable<PostEntity>> GetAll()
        {
            var posts = _context.Posts
              .Include(p => p.AuthorUser)
              .Include(p => p.Categories)
              .ToListAsync();
            return await posts;
        }

        public async Task<IEnumerable<PostEntity>> GetByCategory(string category)
        {
            var allPosts = await GetAll();
            var postsByCategory = allPosts
                .Where(p => p.Categories.Any(c => c.CategoryName.Equals(category)))
                .OrderByDescending(p => p.CreatedDate);
            return postsByCategory;
        }
        public async Task<IEnumerable<PostEntity>> GetByCity(string city, UserEntity loginUser)
        {
            var allPosts = await GetAll();
            
                var postsByCity = allPosts
                    .Where(p => p.Location.Equals(loginUser.City))
                    .OrderByDescending(p => p.CreatedDate)
                    .ToList();
                return postsByCity;
            
        
        }

        public async Task<IEnumerable<PostEntity>> GetByContain(string search)
        {
            var posts = _context.Posts
                 .Include(p => p.AuthorUser)
                 .Where(p => p.Title.Contains(search))
                 .ToListAsync();
            return await posts;
        }

        public async Task<PostEntity> GetByIdAsync(int id)
        {

            await _context.Posts
                .Include(p => p.Categories)
                .Include(p => p.AuthorUser)
                .FirstOrDefaultAsync(i => i.Id == id);
            return await _context.Posts.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(PostEntity post)
        {
            throw new NotImplementedException();
        }
    }
}
