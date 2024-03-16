using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Areas.Identity.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
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

    }

}
