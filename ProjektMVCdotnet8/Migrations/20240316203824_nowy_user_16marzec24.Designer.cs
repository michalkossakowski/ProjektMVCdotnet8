﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjektMVCdotnet8.Areas.Identity.Data;

#nullable disable

namespace ProjektMVCdotnet8.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240316203824_nowy_user_16marzec24")]
    partial class nowy_user_16marzec24
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nick")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserEntity");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.BlockedUserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BlockedUserId")
                        .HasColumnType("int");

                    b.Property<int>("BlockingUserId")
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

                    b.Property<int>("AuthorUserId")
                        .HasColumnType("int");

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

                    b.Property<int>("FollowedUserId")
                        .HasColumnType("int");

                    b.Property<int>("FollowingUserId")
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

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsedChatId")
                        .HasColumnType("int");

                    b.Property<int>("UsingUserId")
                        .HasColumnType("int");

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
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "BlockedUser")
                        .WithMany()
                        .HasForeignKey("BlockedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "BlockingUser")
                        .WithMany()
                        .HasForeignKey("BlockingUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BlockedUser");

                    b.Navigation("BlockingUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.ChatEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "ChattingUser1")
                        .WithMany()
                        .HasForeignKey("ChattingUser1Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "ChattingUser2")
                        .WithMany()
                        .HasForeignKey("ChattingUser2Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChattingUser1");

                    b.Navigation("ChattingUser2");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.CommentEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "AuthorUser")
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
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "FollowedUser")
                        .WithMany()
                        .HasForeignKey("FollowedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "FollowingUser")
                        .WithMany()
                        .HasForeignKey("FollowingUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

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
                        .HasForeignKey("UsingUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UsedChat");

                    b.Navigation("UsingUser");
                });

            modelBuilder.Entity("ProjektMVCdotnet8.Entities.PostEntity", b =>
                {
                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "AuthorUser")
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

                    b.HasOne("ProjektMVCdotnet8.Areas.Identity.Data.UserEntity", "ReactingUser")
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
