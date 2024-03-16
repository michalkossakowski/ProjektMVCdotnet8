﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjektMVCdotnet8.Areas.Identity.Data;

#nullable disable

namespace ProjektMVCdotnet8.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryEntityPostEntity", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("PostsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "PostsId");

                    b.HasIndex("PostsId");

                    b.ToTable("CategoryEntityPostEntity (Dictionary<string, object>)");
                });

<<<<<<< HEAD
            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");
=======
            modelBuilder.Entity("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
>>>>>>> 92710e91b22b767df82cc23001c247bb786508b9

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

<<<<<<< HEAD
                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");
=======
                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
>>>>>>> 92710e91b22b767df82cc23001c247bb786508b9

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

<<<<<<< HEAD
=======
                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

>>>>>>> 92710e91b22b767df82cc23001c247bb786508b9
                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nick")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
<<<<<<< HEAD
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");
=======
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
>>>>>>> 92710e91b22b767df82cc23001c247bb786508b9

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

<<<<<<< HEAD
=======
                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

>>>>>>> 92710e91b22b767df82cc23001c247bb786508b9
                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
<<<<<<< HEAD
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
=======
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserEntity");
>>>>>>> 92710e91b22b767df82cc23001c247bb786508b9
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.BlockedUserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BlockedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BlockingUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BlockedUserId");

                    b.HasIndex("BlockingUserId");

                    b.ToTable("BlockedUserEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ChatEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ChattingUser1Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChattingUser2Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ChattingUser1Id");

                    b.HasIndex("ChattingUser2Id");

                    b.ToTable("ChatEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.CommentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CommentedPostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorUserId");

                    b.HasIndex("CommentedPostId");

                    b.ToTable("CommentEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ContactEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ContactDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ContactType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContactEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.FollowUserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FollowedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FollowingUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("FollowedUserId");

                    b.HasIndex("FollowingUserId");

                    b.ToTable("FollowUserEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.MessageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsedChatId")
                        .HasColumnType("int");

                    b.Property<string>("UsingUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UsedChatId");

                    b.HasIndex("UsingUserId");

                    b.ToTable("MessageEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.PostEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AttachmentSource")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorUserId");

                    b.ToTable("PostEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ReactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ReactedPostId")
                        .HasColumnType("int");

                    b.Property<string>("ReactingUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReactionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReactedPostId");

                    b.HasIndex("ReactingUserId");

                    b.ToTable("ReactionEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ReportPostEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ReportContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReportedPostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportedPostId");

                    b.ToTable("ReportPostEntity");
                });

            modelBuilder.Entity("CategoryEntityPostEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.CategoryEntity", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjektMVCdotnet8.Entities.PostEntity", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
<<<<<<< HEAD
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
=======
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "BlockedUser")
>>>>>>> 92710e91b22b767df82cc23001c247bb786508b9
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

<<<<<<< HEAD
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", null)
=======
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "BlockingUser")
>>>>>>> 92710e91b22b767df82cc23001c247bb786508b9
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.BlockedUserEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "BlockedUser")
                        .WithMany()
                        .HasForeignKey("BlockedUserId");

                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "BlockingUser")
                        .WithMany()
                        .HasForeignKey("BlockingUserId");

                    b.Navigation("BlockedUser");

                    b.Navigation("BlockingUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ChatEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "ChattingUser1")
                        .WithMany()
                        .HasForeignKey("ChattingUser1Id");

                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "ChattingUser2")
                        .WithMany()
                        .HasForeignKey("ChattingUser2Id");

                    b.Navigation("ChattingUser1");

                    b.Navigation("ChattingUser2");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.CommentEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "AuthorUser")
                        .WithMany()
                        .HasForeignKey("AuthorUserId");

                    b.HasOne("ProjektMVCdotnet8.Entities.PostEntity", "CommentedPost")
                        .WithMany()
                        .HasForeignKey("CommentedPostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AuthorUser");

                    b.Navigation("CommentedPost");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.FollowUserEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "FollowedUser")
                        .WithMany()
                        .HasForeignKey("FollowedUserId");

                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "FollowingUser")
                        .WithMany()
                        .HasForeignKey("FollowingUserId");

                    b.Navigation("FollowedUser");

                    b.Navigation("FollowingUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.MessageEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.ChatEntity", "UsedChat")
                        .WithMany()
                        .HasForeignKey("UsedChatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "UsingUser")
                        .WithMany()
                        .HasForeignKey("UsingUserId");

                    b.Navigation("UsedChat");

                    b.Navigation("UsingUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.PostEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "AuthorUser")
                        .WithMany()
                        .HasForeignKey("AuthorUserId");

                    b.Navigation("AuthorUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ReactionEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.PostEntity", "ReactedPost")
                        .WithMany()
                        .HasForeignKey("ReactedPostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "ReactingUser")
                        .WithMany()
                        .HasForeignKey("ReactingUserId");

                    b.Navigation("ReactedPost");

                    b.Navigation("ReactingUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ReportPostEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.PostEntity", "ReportedPost")
                        .WithMany()
                        .HasForeignKey("ReportedPostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ReportedPost");
                });
#pragma warning restore 612, 618
        }
    }
}
