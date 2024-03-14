using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektMVCdotnet8.Migrations
{
    /// <inheritdoc />
    public partial class test_3_14marzec24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlockedUserEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockingUserId = table.Column<int>(type: "int", nullable: false),
                    BlockedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedUserEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockedUserEntity_UserEntity_BlockedUserId",
                        column: x => x.BlockedUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlockedUserEntity_UserEntity_BlockingUserId",
                        column: x => x.BlockingUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChattingUser1Id = table.Column<int>(type: "int", nullable: false),
                    ChattingUser2Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatEntity_UserEntity_ChattingUser1Id",
                        column: x => x.ChattingUser1Id,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatEntity_UserEntity_ChattingUser2Id",
                        column: x => x.ChattingUser2Id,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FollowUserEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowingUserId = table.Column<int>(type: "int", nullable: false),
                    FollowedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUserEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUserEntity_UserEntity_FollowedUserId",
                        column: x => x.FollowedUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowUserEntity_UserEntity_FollowingUserId",
                        column: x => x.FollowingUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorUserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttachmentSource = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostEntity_UserEntity_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsingUserId = table.Column<int>(type: "int", nullable: false),
                    UsedChatId = table.Column<int>(type: "int", nullable: false),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageEntity_ChatEntity_UsedChatId",
                        column: x => x.UsedChatId,
                        principalTable: "ChatEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageEntity_UserEntity_UsingUserId",
                        column: x => x.UsingUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEntityPostEntity (Dictionary<string, object>)",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntityPostEntity (Dictionary<string, object>)", x => new { x.CategoriesId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_CategoryEntityPostEntity (Dictionary<string, object>)_CategoryEntity_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "CategoryEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryEntityPostEntity (Dictionary<string, object>)_PostEntity_PostsId",
                        column: x => x.PostsId,
                        principalTable: "PostEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorUserId = table.Column<int>(type: "int", nullable: false),
                    CommentedPostId = table.Column<int>(type: "int", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentEntity_PostEntity_CommentedPostId",
                        column: x => x.CommentedPostId,
                        principalTable: "PostEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentEntity_UserEntity_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReactionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReactingUserId = table.Column<int>(type: "int", nullable: false),
                    ReactedPostId = table.Column<int>(type: "int", nullable: false),
                    ReactionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReactionEntity_PostEntity_ReactedPostId",
                        column: x => x.ReactedPostId,
                        principalTable: "PostEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReactionEntity_UserEntity_ReactingUserId",
                        column: x => x.ReactingUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportPostEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportedPostId = table.Column<int>(type: "int", nullable: false),
                    ReportContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPostEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportPostEntity_PostEntity_ReportedPostId",
                        column: x => x.ReportedPostId,
                        principalTable: "PostEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUserEntity_BlockedUserId",
                table: "BlockedUserEntity",
                column: "BlockedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUserEntity_BlockingUserId",
                table: "BlockedUserEntity",
                column: "BlockingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntityPostEntity (Dictionary<string, object>)_PostsId",
                table: "CategoryEntityPostEntity (Dictionary<string, object>)",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntity_ChattingUser1Id",
                table: "ChatEntity",
                column: "ChattingUser1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntity_ChattingUser2Id",
                table: "ChatEntity",
                column: "ChattingUser2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_AuthorUserId",
                table: "CommentEntity",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_CommentedPostId",
                table: "CommentEntity",
                column: "CommentedPostId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUserEntity_FollowedUserId",
                table: "FollowUserEntity",
                column: "FollowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUserEntity_FollowingUserId",
                table: "FollowUserEntity",
                column: "FollowingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageEntity_UsedChatId",
                table: "MessageEntity",
                column: "UsedChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageEntity_UsingUserId",
                table: "MessageEntity",
                column: "UsingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntity_AuthorUserId",
                table: "PostEntity",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionEntity_ReactedPostId",
                table: "ReactionEntity",
                column: "ReactedPostId");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionEntity_ReactingUserId",
                table: "ReactionEntity",
                column: "ReactingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPostEntity_ReportedPostId",
                table: "ReportPostEntity",
                column: "ReportedPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockedUserEntity");

            migrationBuilder.DropTable(
                name: "CategoryEntityPostEntity (Dictionary<string, object>)");

            migrationBuilder.DropTable(
                name: "CommentEntity");

            migrationBuilder.DropTable(
                name: "ContactEntity");

            migrationBuilder.DropTable(
                name: "FollowUserEntity");

            migrationBuilder.DropTable(
                name: "MessageEntity");

            migrationBuilder.DropTable(
                name: "ReactionEntity");

            migrationBuilder.DropTable(
                name: "ReportPostEntity");

            migrationBuilder.DropTable(
                name: "CategoryEntity");

            migrationBuilder.DropTable(
                name: "ChatEntity");

            migrationBuilder.DropTable(
                name: "PostEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
