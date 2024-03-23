using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using Microsoft.AspNetCore.Identity;
using ProjektMVCdotnet8.Areas.Identity.Data;
namespace ProjektMVCdotnet8.Controllers
{
    public class PostController : Controller
    {

        private ApplicationDbContext _context;
        
        UserManager<UserEntity> _userManager;
        public PostController(ApplicationDbContext context, UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string CategoryName)
        {
            TempData["CategoryName"] = CategoryName;
            /* var blockedUsers = _context.BlockedUsers.Where(blockUser => blockUser.BlockingUser.Id.Equals(usermanager.GetUserId(User)))
                 ;*/

            var blockedUsers = _context.BlockedUsers
                 .Where(entry => entry.BlockingUser.Id == _userManager.GetUserId(User))
                 .Select(entry => entry.BlockedUser.Id)
                 .ToList();
            var posts = _context.Posts.
                Include(p => p.AuthorUser)
                .Include(p => p.Categories)
                .Where(post => post.Categories.Any(category => category.CategoryName.Equals(CategoryName)))
                .OrderByDescending(post => post.CreatedDate)
                .ToList();
            var filteredPosts = posts
                .Where(post => !blockedUsers.Contains(post.AuthorUser.Id))
                .ToList();
            List<CommentEntity> comments = new List<CommentEntity> ();
            comments = _context.Comments.ToList();
            ViewBag.Comments = comments;
            return View("Index", filteredPosts);
        }

        public async Task<IActionResult> Block(string BlockedUserID, string CategoryName, string BlockingUser)
        {
            UserEntity userToBlock = _context.Users.FirstOrDefault(user => user.Id.Equals(BlockedUserID));
            UserEntity blockingUser = _context.Users.FirstOrDefault(user => user.Id.Equals(BlockingUser));
            BlockedUserEntity blockedUser = new BlockedUserEntity(blockingUser, userToBlock);
            _context.Add(blockedUser);
            _context.SaveChangesAsync();
            return RedirectToAction("Index", new { CategoryName });
        }
        public async Task<IActionResult> Follow(int id, string CategoryName)
        {
            return RedirectToAction("Index", new { CategoryName });
        }
        public async Task<IActionResult> AddComment()
        {
            CommentEntity comment = new CommentEntity();
            comment.CommentContent = Request.Form["commentContent"];
            var user = await _userManager.GetUserAsync(User);
            comment.userNick = _userManager.GetUserName(User);
            comment.AuthorUser = user;
            comment.CreatedDate = DateTime.Now;
            var post = _context.Posts.FirstOrDefault(c => c.Id == int.Parse(Request.Form["postId"]));
            comment.CommentedPost = post;

            comment.postId = post.Id;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            string CategoryName = Request.Form["category"];
            //Console.WriteLine("Uzytkownik dodajacy: "+ user.Nick);
            return RedirectToAction("Index", new { CategoryName  } );
        }
        /*public  ActionResult Comments(int postId)
        {
            var post = _context.Posts.FirstOrDefault(c => c.Id == postId);
            var comments = _context.Comments.
                Where(entry => entry.CommentedPost == post)
                .ToList();

            return PartialView(comments);
        }*/
    }
}
