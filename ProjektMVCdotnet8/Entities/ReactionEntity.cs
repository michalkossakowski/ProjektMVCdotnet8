namespace ProjektMVCdotnet8.Entities
{
    public class ReactionEntity
    {
        public int Id { get; set; }
        //public UserEntity ReactingUser { get; set; }
        public PostEntity ReactedPost { get; set; }
        public string ReactionName { get; set; }
    }
}
