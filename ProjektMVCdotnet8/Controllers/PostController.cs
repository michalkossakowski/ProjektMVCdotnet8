using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
namespace ProjektMVCdotnet8.Controllers
{
    public class PostController : Controller
    {

        public ApplicationDbContext _context;
        SignInManager<UserEntity> _signInManager;
        UserManager<UserEntity> _userManager;
        public PostController(ApplicationDbContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }


        public async Task<IActionResult> Index(string CategoryName)
        {
            //Przesyła informacje jakie posty będzie wyświetlał na stronie według kategorii
            TempData["CategoryName"] = CategoryName;

            var blockedUsers = _context.BlockedUsers
                    .Where(entry => entry.BlockingUser.Id == _userManager.GetUserId(User))
                    .Select(entry => entry.BlockedUser.Id)
                    .ToList();

            var posts = _context.Posts
                    .Include(p => p.AuthorUser)
                    .Include(p => p.Categories)
                    .Where(post => post.Categories.Any(category => category.CategoryName.Equals(CategoryName)))
                    .OrderByDescending(post => post.CreatedDate)
                    .ToList();

            List<CommentEntity> comments = new List<CommentEntity>();
            comments = _context.Comments
                    .Include(comment => comment.AuthorUser)
                    .Include(comment => comment.CommentedPost)
                    .ToList();
            ViewBag.Comments = comments;
            
            if (_signInManager.IsSignedIn(User))
            {
                var filteredPosts = posts
                    .Where(post => !blockedUsers.Contains(post.AuthorUser.Id))
                    .ToList();
                var followedUsers = _context.FollowUsers
                    .Where(user => user.FollowingUser.Id == _userManager.GetUserId(User))
                    .Select(user => user.FollowedUser.Id)
                    .ToList();
                TempData["FollowedUsers"] = followedUsers;
                return View("Index", filteredPosts);
            }
            else
            {
                TempData["FollowedUsers"] = "null";
                return View("Index", posts);
            }
        }


        //Blokuje danego użytkownika
        public async Task<IActionResult> Block(string BlockedUserID, string CategoryName)
        {
            UserEntity userToBlock = _context.Users.FirstOrDefault(user => user.Id.Equals(BlockedUserID));
            UserEntity blockingUser = _context.Users.FirstOrDefault(user => user.Id.Equals(_userManager.GetUserId(User)));
            BlockedUserEntity blockedUser = new BlockedUserEntity(blockingUser, userToBlock);
            _context.Add(blockedUser);
            await _context.SaveChangesAsync();
            string FollowedUserID = BlockedUserID;
            //Po zablokowaniu użytkownika także go przestaje obserwować
            return RedirectToAction("UnFollow", new { FollowedUserID, CategoryName });
        }

        //Obserowanie dane użytkownika
        public async Task<IActionResult> Follow(string FollowedUserID, string CategoryName)
        {
            UserEntity userToFollow = _context.Users.FirstOrDefault(user => user.Id.Equals(FollowedUserID));
            UserEntity followingUser = _context.Users.FirstOrDefault(user => user.Id.Equals(_userManager.GetUserId(User)));
            FollowUserEntity followedUser = new FollowUserEntity(followingUser, userToFollow);
            _context.Add(followedUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { CategoryName });
        }


        //Zaprzestanie obserwowania danego użytkownika
        public async Task<IActionResult> UnFollow(string FollowedUserID, string CategoryName)
        {
            var userToUnfollow = _context.FollowUsers
                .Include(user => user.FollowedUser)
                .Include(user => user.FollowingUser)
                .Where(user => user.FollowedUser.Id.Equals(FollowedUserID) && user.FollowingUser.Id.Equals(_userManager.GetUserId(User)))
                .FirstOrDefault();
            _context.FollowUsers.Remove(userToUnfollow);
            await _context.SaveChangesAsync();
            if (CategoryName is null)
            {
                return RedirectToAction("Followed");
            }
            else
            {
                return RedirectToAction("Index", new { CategoryName });
            }
        }

        //Wyświetla strone z postami obserwowanych użytkowników
        public async Task<IActionResult> Followed()
        {

            List<CommentEntity> comments = new List<CommentEntity>();
            comments = _context.Comments
                    .Include(comment => comment.AuthorUser)
                    .Include(comment => comment.CommentedPost)
                    .ToList();
            ViewBag.Comments = comments;
            var followedUsers = _context.FollowUsers
                .Where(entry => entry.FollowingUser.Id == _userManager.GetUserId(User))
                .Select(entry => entry.FollowedUser.Id)
                .ToList();
            var posts = _context.Posts
                .Include(p => p.AuthorUser)
                .Include(p => p.Categories)
                .OrderByDescending(post => post.CreatedDate)
                .ToList();
            var filteredPosts = posts
                .Where(post => followedUsers.Contains(post.AuthorUser.Id))
                .ToList();
            TempData["FollowedUsers"] = followedUsers;
            return View("Index", filteredPosts);
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
            if (CategoryName == "")
            {
                return RedirectToAction("Followed");
            }
            else 
            {
                return RedirectToAction("Index", new { CategoryName });

            }
        }
        public async Task<IActionResult> ShowPost(int Id)
        {
            var gsdsa = _context.Posts;
            var post = _context.Posts
                .Where(x => x.Id == Id)
                .FirstOrDefault();
            if (post == null) ;
            return View("ShowPost", post);
        }
        public async Task<IActionResult> LivePostSearch(string search)
        {
            List<PostEntity> res =  _context.Posts
               .Where(p => p.Title.Contains(search))
               .ToList();
            return PartialView("LivePostSearch", res);
        }

        public  ActionResult LivePostSearch(string search)
        {
            List<PostEntity> res =  _context.Posts
               .Where(p => p.Title.Contains(search))
               .ToList();
            _context.Dispose();
            return PartialView("LivePostSearch", res);
        }

        public async Task<IActionResult> ShowPost(string Id)
        {
            var post = _context.Posts
                .Where(x => x.Id.Equals(Id))
                .FirstOrDefault();
            if (post == null) ;
            return View("ShowPost", post);
        }
    }
}