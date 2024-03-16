using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public UserEntity AuthorUser { get; set; }
        public PostEntity CommentedPost { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
