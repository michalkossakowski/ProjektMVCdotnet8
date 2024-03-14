﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjektMVCdotnet8.Entities;

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

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.BlockedUserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BlockedUserId")
                        .HasColumnType("int");

                    b.Property<int?>("BlockingUserId")
                        .HasColumnType("int");

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

                    b.Property<int>("ChattingUser1Id")
                        .HasColumnType("int");

                    b.Property<int>("ChattingUser2Id")
                        .HasColumnType("int");

                    b.Property<int?>("User1Id")
                        .HasColumnType("int");

                    b.Property<int?>("User2Id")
                        .HasColumnType("int");

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

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorUserId")
                        .HasColumnType("int");

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CommentedPostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

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

                    b.Property<int?>("FollowedUserId")
                        .HasColumnType("int");

                    b.Property<int?>("FollowingUserId")
                        .HasColumnType("int");

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

                    b.Property<int>("ChatIdId")
                        .HasColumnType("int");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserIdId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatIdId");

                    b.HasIndex("UserIdId");

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

                    b.Property<int>("AuthorUserId")
                        .HasColumnType("int");

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

                    b.Property<int>("ReactingUserId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nick")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserEntity");
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

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.BlockedUserEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "BlockedUser")
                        .WithMany()
                        .HasForeignKey("BlockedUserId");

                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "BlockingUser")
                        .WithMany()
                        .HasForeignKey("BlockingUserId");

                    b.Navigation("BlockedUser");

                    b.Navigation("BlockingUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ChatEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "ChattingUser1")
                        .WithMany()
                        .HasForeignKey("ChattingUser1Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "ChattingUser2")
                        .WithMany()
                        .HasForeignKey("ChattingUser2Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChattingUser1");

                    b.Navigation("ChattingUser2");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.CommentEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "AuthorUser")
                        .WithMany()
                        .HasForeignKey("AuthorUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

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
                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "FollowedUser")
                        .WithMany()
                        .HasForeignKey("FollowedUserId");

                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "FollowingUser")
                        .WithMany()
                        .HasForeignKey("FollowingUserId");

                    b.Navigation("FollowedUser");

                    b.Navigation("FollowingUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.MessageEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.ChatEntity", "ChatId")
                        .WithMany()
                        .HasForeignKey("ChatIdId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "UserId")
                        .WithMany()
                        .HasForeignKey("UserIdId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChatId");

                    b.Navigation("UserId");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.PostEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "AuthorUser")
                        .WithMany()
                        .HasForeignKey("AuthorUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AuthorUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ReactionEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Entities.PostEntity", "ReactedPost")
                        .WithMany()
                        .HasForeignKey("ReactedPostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjektMVCdotnet8.Entities.UserEntity", "ReactingUser")
                        .WithMany()
                        .HasForeignKey("ReactingUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

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
