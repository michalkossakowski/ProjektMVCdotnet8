namespace ProjektMVCdotnet8.Models
{
    public class Post
    {
        public Guid Id { get; set; } = default!;
        public int AuthorID { get; set; } = default!;
        public int CategoryId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string? Contents { get; set; }
        public DateTime PublishedDate { get; set; }
        public ICollection <Categorie> Categories { get; set; }

    }
}
