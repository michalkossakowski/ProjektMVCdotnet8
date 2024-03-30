using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<ReportPostEntity>> GetAll();
        Task<ReportPostEntity> GetById(int id);
        bool Add(ReportPostEntity contact);
        bool Delete(ReportPostEntity contact);
        bool DeleteById(int id);
        bool Save();
    }
}
