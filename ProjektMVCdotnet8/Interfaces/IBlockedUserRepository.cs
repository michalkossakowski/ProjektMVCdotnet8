using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IBlockedUserRepository
    {
        // Zwraca wszystkich blokowanych użytkowników przez danego użytkownika (w domyśle zalogowanego)
        Task<IEnumerable<BlockedUserEntity>> GetAllBlockedBy(string userSingedID);

        // Zwraca ID(string) wszystkich blokowanych użytkowników przez danego użytkownika (w domyśle zalogowanego)
        Task<IEnumerable<string>> GetAllIDBlockedBy(string userSingedID);

        // Dodaje blokowanie nowego użytkownika do bazy danych
        bool Add(BlockedUserEntity post);

        // Usuwa blokade na użytkowniku
        bool Delete(BlockedUserEntity blockedUser);

        // Usuwa blokade na użytkowniku
        bool Delete(int id);

        // Zapisuje zmiany w bazie danych
        bool Save();
    }
}
