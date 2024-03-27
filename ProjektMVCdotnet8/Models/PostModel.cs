using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public UserEntity AuthorUser { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public IFormFile AttachmentSource { get; set; }
        public string? Location { get; set; }

        public List<CategoryEntity>? Categories = new List<CategoryEntity>();
    }
}
