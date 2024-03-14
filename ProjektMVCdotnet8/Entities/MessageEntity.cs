namespace ProjektMVCdotnet8.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public UserEntity UserId { get; set; }
        public ChatEntity ChatId { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendDate { get; set; }
    }
}
