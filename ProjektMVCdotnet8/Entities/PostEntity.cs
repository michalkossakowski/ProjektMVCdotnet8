namespace ProjektMVCdotnet8.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        //public UserEntity AuthorUser { get; set; }
        public string Title {  get; set; }
        public string PostContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AttachmentSource { get; set; }
        public ICollection <CategoryEntity> Categories { get; set; }
    }
}
