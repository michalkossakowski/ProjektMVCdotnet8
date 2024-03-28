using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface ICommentRepository
    {
        Task<CommentEntity> MapCommentEntity(CommentEntity commentEntity, UserEntity user, PostEntity post, string userSignedNick);
        Task<IEnumerable<CommentEntity>> GetAll();
        bool Add(CommentEntity post);
        bool Save();

    }
}
