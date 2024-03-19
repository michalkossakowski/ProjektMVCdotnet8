namespace ProjektMVCdotnet8.Entities
{
    public class ReportPostEntity
    {
        public int Id { get; set; }
        public PostEntity ReportedPost { get; set; }
        public int postId {  get; set; }
        public string ReportContent { get; set; }
    }
}
