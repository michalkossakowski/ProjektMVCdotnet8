namespace ProjektMVCdotnet8.Entities
{
    public class ContactEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Topic { get; set; }
        public string ContactType { get; set; }
        public string ContactContent { get; set; }
        public DateTime ContactDate { get; set; }
    }
}
