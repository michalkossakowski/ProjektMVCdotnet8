using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface ICommentRepository
    {
        // Mapowanie komentarza 
        Task<CommentEntity> MapCommentEntity(CommentEntity commentEntity, UserEntity user, PostEntity post, string userSignedNick);
        
        // Zwraca wszystkie komentarze
        Task<IEnumerable<CommentEntity>> GetAll();

        // Dodaje komentarz 
        bool Add(CommentEntity post);

        // Zapisuje zmiany w bazie danych
        bool Save();

    }
}
