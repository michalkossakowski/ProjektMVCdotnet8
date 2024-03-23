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
        public async Task<IActionResult> Follow(string FollowedUserID, string CategoryName, string FollowingUserID)
        {
            UserEntity userToFollow = _context.Users.FirstOrDefault(user => user.Id.Equals(FollowedUserID));
            UserEntity followingUser = _context.Users.FirstOrDefault(user => user.Id.Equals(FollowingUserID));
            FollowUserEntity followedUser = new FollowUserEntity(followingUser, userToFollow);
            _context.Add(followedUser);
            _context.SaveChangesAsync();
            return RedirectToAction("Index", new { CategoryName });
        }

        public async Task<IActionResult> UnFollow(string FollowedUserID, string CategoryName, string FollowingUserID)
        {

            return RedirectToAction("Index", new { CategoryName });
        }

        public async Task<IActionResult> Followed()
        {
            var followedUsers = _context.FollowUsers
                 .Where(entry => entry.FollowingUser.Id == _userManager.GetUserId(User))
                 .Select(entry => entry.FollowedUser.Id)
                 .ToList();
            var posts = _context.Posts.
                Include(p => p.AuthorUser)
                .Include(p => p.Categories)
                .OrderByDescending(post => post.CreatedDate)
                .ToList();
            var filteredPosts = posts
                .Where(post => followedUsers.Contains(post.AuthorUser.Id))
                .ToList();
            return View("Index", filteredPosts);
        }


    }
}
