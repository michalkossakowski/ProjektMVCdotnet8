namespace ProjektMVCdotnet8.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection <PostEntity> Posts { get; set; }

       
        public CategoryEntity(string CategoryName) 
        {
            this.CategoryName = CategoryName;
        }
        public CategoryEntity()
        {

        }
    }
}
