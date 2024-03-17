using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        public UserEntity AuthorUser { get; set; }
        public string Title {  get; set; }
        public string PostContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? AttachmentSource { get; set; }
        public ICollection <CategoryEntity> Categories { get; set; }

        public PostEntity() { }
        public PostEntity(UserEntity AuthorUser, string Title, string PostContent, string AttachmentSource, ICollection<CategoryEntity> Categories) 
        {
            this.AuthorUser = AuthorUser;
            this.Title = Title;
            this.PostContent = PostContent;
            this.CreatedDate = DateTime.Now;
            this.AttachmentSource = AttachmentSource;
            this.Categories = Categories;
        }


    }
}
