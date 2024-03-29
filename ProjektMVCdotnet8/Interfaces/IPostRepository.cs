using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
namespace ProjektMVCdotnet8.Interfaces
{ 
    public interface IPostRepository
    {
        // Zwraca wszystkie posty z bazy danych
        Task<IEnumerable<PostEntity>> GetAll();

        // Zwraca post po id
        Task<PostEntity> GetById(int id);

        // Zwraca wszystkie posty z danej kategorii
        Task<IEnumerable<PostEntity>> GetByCategory(string category);

        // Zwraca wszystkie posty z danej lokacji użytkownika
        Task<IEnumerable<PostEntity>> GetByCity(string city, UserEntity loginUser);

        // Zwraca wszystkie posty z danej lokacji użytkownika
        Task<IEnumerable<PostEntity>> GetByContain(string search);

        // Dodaje posty
        bool Add(PostEntity post);

        // Usuwa posty
        bool Delete(PostEntity post);

        // Usuwa posty
        bool Delete(int id);

        // Zapisuje zmiany do bazy danych
        bool Save();
    }
}
