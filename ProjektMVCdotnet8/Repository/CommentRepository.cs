using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;
using System.Xml.Linq;

namespace ProjektMVCdotnet8.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context) 
        {
            this._context = context;
        }

        public bool Add(CommentEntity comment)
        {
            _context.Add(comment);
            return Save();
        }
        public async Task<IEnumerable<CommentEntity>> GetAll()
        {
            var comments = _context.Comments
                .Include(c => c.AuthorUser)
                .Include(c => c.CommentedPost)
                .ToListAsync();
            return await comments;
        }

        public async Task<IEnumerable<CommentEntity>> GetByPostID(int ID) 
        {
            var comments = _context.Comments
                .Where(c => c.postId ==ID)
                .Include(c => c.AuthorUser)
                .Include(c => c.CommentedPost)
                .ToListAsync();
            return await comments;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<CommentEntity> MapCommentEntity(CommentEntity commentEntity, UserEntity user, PostEntity post, string userSignedNick)
        {
            commentEntity.userNick = userSignedNick;
            commentEntity.AuthorUser = user;
            commentEntity.CreatedDate = DateTime.Now;
            commentEntity.CommentedPost = post;
            commentEntity.postId = post.Id;
            user.Points += 500;
            return commentEntity;
        }
    }
}
