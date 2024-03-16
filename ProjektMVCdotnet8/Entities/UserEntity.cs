namespace ProjektMVCdotnet8.Entities
{
    public class UserEntity
    {
        // to zrobi entity framework - Identity
        public int Id { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime JoinDate { get; set; }
        public string Avatar {  get; set; }
        public int Points { get; set; } // my musimy dodać reszta jest w identity
    }
}
