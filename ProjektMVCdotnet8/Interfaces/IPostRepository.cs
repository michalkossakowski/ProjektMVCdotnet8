using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
namespace ProjektMVCdotnet8.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostEntity>> GetAll();
        Task<PostEntity> GetByIdAsync(int id);
        Task<IEnumerable<PostEntity>> GetByCategory(string category);
        Task<IEnumerable<PostEntity>> GetByCity(string city, UserEntity loginUser);
        Task<IEnumerable<PostEntity>> GetByContain(string search);
        bool Add(PostEntity post);
        bool Delete(PostEntity post);
        bool Delete(int id);
        bool Update(PostEntity post);
        bool Save();
    }
}
