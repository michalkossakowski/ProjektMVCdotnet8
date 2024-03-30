using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
namespace ProjektMVCdotnet8.Interfaces
{
    public interface IFollowUserRepository
    {
        // Zwraca wszystkie obiekty 
        Task<IEnumerable<FollowUserEntity>> GetAll();

        // Zwraca wszystkich obserwowanych użytkowników wyszukąc dla osoby obserwującej (string id)
        Task<IEnumerable<UserEntity>> GetAllFollowedBY(string id);

        // Zwraca użytkownika pasujący do użytkownika obserwowanego oraz jednocześnie zalogowanego użytkownika
        Task<UserEntity> GetByIdFollowedUser(string followedUser, string userSignedID);

        // Zwraca obiekt pasujący do użytkownika obserwowanego oraz jednocześnie zalogowanego użytkownika
        Task<FollowUserEntity> GetById(string followedUser, string userSignedID);

        // Dodaje obiekt
        bool Add(FollowUserEntity post);

        // Usuwanie obiektu
        bool Delete(FollowUserEntity post);

        // Zapisywanie zmian do bazy danych
        bool Save();
    }
}
