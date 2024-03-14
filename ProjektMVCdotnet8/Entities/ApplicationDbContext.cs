using Microsoft.EntityFrameworkCore;

namespace ProjektMVCdotnet8.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Init();
        }
        public DbSet<BlockedUserEntity> BlockedUsers { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<ContactEntity> ContactForms { get; set; }
        public DbSet<FollowUserEntity> FollowUsers { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<ReactionEntity> Reactions { get; set; }
        public DbSet<ReportPostEntity> ReportPosts { get; set; }
        public DbSet<UserEntity> Users { get; set; } // tymaczasowo

        // umożliwia dwa pola tej samej customowej klasy w tabeli
        protected override void OnModelCreating(ModelBuilder mb)
        {
            foreach (var Entity in mb.Model.GetEntityTypes())
            {
                Entity.SetTableName(Entity.DisplayName());
                Entity.GetForeignKeys()
                    .Where(x => !x.IsOwnership && x.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(x => x.DeleteBehavior = DeleteBehavior.Restrict);
            }
            base.OnModelCreating(mb);
        }

        // testowe dane do tabel
        public void Init()
        {
            var newContactForm = new ContactEntity { Email = "John@jhonny.com", Topic = "Bardzo mocno pada", ContactType = "problem", ContactContent = "Wyszedł na dwór i padało", ContactDate = DateTime.Now };
            ContactForms.Add(newContactForm);

            var newContactForm2 = new ContactEntity { Email = "John2@jhonny.com", Topic = "Bardzo mocno pada", ContactType = "problem", ContactContent = "Wyszedł na dwór i padało", ContactDate = DateTime.Now };
            ContactForms.Add(newContactForm2);


            SaveChanges();
            Console.WriteLine("Wykonano init bazy danych.");

        }
    }

}
