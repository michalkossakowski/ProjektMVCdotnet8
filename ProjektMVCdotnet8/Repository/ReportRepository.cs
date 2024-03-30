using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektMVCdotnet8.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ReportPostEntity report)
        {
            _context.Add(report);
            return Save();
        }

        public bool Delete(ReportPostEntity report)
        {
            _context.Remove(report);
            return Save();
        }

        public bool DeleteById(int id)
        {
            var report = _context.ReportPosts.Find(id);
            _context.ReportPosts.Remove(report);
            return Save();
        }

        public async Task<IEnumerable<ReportPostEntity>> GetAll()
        {
            return await _context.ReportPosts.ToListAsync();
        }

        public async Task<ReportPostEntity> GetById(int id)
        {
            return await _context.ReportPosts.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
