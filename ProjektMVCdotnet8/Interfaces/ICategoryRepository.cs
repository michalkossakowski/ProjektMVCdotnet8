using ProjektMVCdotnet8.Entities;
namespace ProjektMVCdotnet8.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryEntity> GetById(int id);
        List<CategoryEntity> GetAllCategories();
    }
}
