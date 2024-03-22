using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public UserEntity UsingUser { get; set; }
        public ChatEntity UsedChat { get; set; }
        public int currentChat { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendDate { get; set; }
    }
}
