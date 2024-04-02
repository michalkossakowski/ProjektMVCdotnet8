using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;

namespace ProjektMVCdotnet8.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryEntity> GetById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public List<CategoryEntity> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
