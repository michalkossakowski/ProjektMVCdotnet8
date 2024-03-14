namespace ProjektMVCdotnet8.Entities
{
    public class ChatEntity
    {
        public int Id { get; set; }
        public int? User1Id { get; set; }
        public int? User2Id { get; set; }
        public UserEntity ChattingUser1 { get; set; }
        public UserEntity ChattingUser2 { get; set; }
    }
}
