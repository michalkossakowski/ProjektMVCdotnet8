using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Entities
{
    public class ChatEntity
    {
        public int Id { get; set; }
        public UserEntity ChattingUser1 { get; set; }
        public string User1Nick { get; set; }
        public UserEntity ChattingUser2 { get; set; }
        public string User2Nick { get; set; }
    }
}
