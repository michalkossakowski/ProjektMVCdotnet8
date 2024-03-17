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
        public List<CategoryEntity> Categories = new List<CategoryEntity>();

/*        public PostModel() 
        {
            CategoryModel categoryModel = new CategoryModel();
            categoryModel.CategoryName = "Elektronika";
            categoryModel.Id = 1;
            categoryModel.isSelected = false;
            Categories.Add(categoryModel);

            categoryModel = new CategoryModel();
            categoryModel.CategoryName = "Programowanie";
            categoryModel.Id = 2;
            Categories.Add(categoryModel);
            categoryModel = new CategoryModel();
            categoryModel.CategoryName = "Komputery";
            categoryModel.Id = 3;
            Categories.Add(categoryModel);
            categoryModel = new CategoryModel();
            categoryModel.CategoryName = "Sieci";
            categoryModel.Id = 4;
            Categories.Add(categoryModel);
            categoryModel = new CategoryModel();
            categoryModel.CategoryName = "Spawanie";
            categoryModel.Id = 5;
            Categories.Add(categoryModel);
            categoryModel = new CategoryModel();
            categoryModel.CategoryName = "Elektryka";
            categoryModel.Id = 6;
            Categories.Add(categoryModel);
        }*/
    }
}
