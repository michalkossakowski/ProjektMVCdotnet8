using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public UserEntity AuthorUser { get; set; }
        public string userNick { get; set; }
        public PostEntity CommentedPost { get; set; }
        public int postId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatedDate { get; set; }

        public CommentEntity() { }
        public CommentEntity(UserEntity authorUser, string userNick, PostEntity commentedPost, int postId, string commentContent, DateTime createdDate)
        {
            this.AuthorUser = authorUser;
            this.userNick = userNick;
            this.CommentedPost = commentedPost;
            this.postId = postId;
            this.CommentContent = commentContent;
            this.CreatedDate = createdDate;
        }
    }
}
