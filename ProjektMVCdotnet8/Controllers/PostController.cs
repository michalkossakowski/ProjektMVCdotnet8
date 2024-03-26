using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using System.Security.Policy;
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


        public async Task<IActionResult> Index(string Information, string site)
        {
            //Przesyła informacje jakie posty będzie wyświetlał na stronie według kategorii
            TempData["Information"] = Information;
            TempData["Site"] = "Index";

            var posts = PostsByCategories(Information);

            List<CommentEntity> comments = GetAllComments();
            ViewBag.Comments = comments;

            if (_signInManager.IsSignedIn(User))
            {
                var blockedUsers = BlockedEntities();
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

        //Wyświetla strone z postami obserwowanych użytkowników
        public async Task<IActionResult> Followed()
        {
            TempData["Site"] = "Followed";
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

        //Zwraca widok z pojędyńczym postem
        public async Task<IActionResult> ShowPost(int Id)
        {

            TempData["Site"] = "ShowPost";
            TempData["Information"] = Id.ToString();

            var post = _context.Posts
                .Where(x => x.Id == Id)
                .FirstOrDefault();
            return View("ShowPost", post);
        }

        //Zwraca widok z postami które wyszukujemy przez wyszukiwarke
        public async Task<IActionResult> FindedPosts(string liveSearchTitle)
        {
            TempData["Information"] = liveSearchTitle;
            TempData["Site"] = "FindedPosts";
            var posts = PostsByContain(liveSearchTitle);

            List<CommentEntity> comments = GetAllComments();
            ViewBag.Comments = comments;

            if (_signInManager.IsSignedIn(User))
            {
                var blockedUsers = BlockedEntities();
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
        public async Task<IActionResult> Block(string BlockedUserID, string Information, string Site)
        {
            UserEntity userToBlock = _context.Users.FirstOrDefault(user => user.Id.Equals(BlockedUserID));
            UserEntity blockingUser = _context.Users.FirstOrDefault(user => user.Id.Equals(_userManager.GetUserId(User)));
            BlockedUserEntity blockedUser = new BlockedUserEntity(blockingUser, userToBlock);
            _context.Add(blockedUser);
            await _context.SaveChangesAsync();
            string FollowedUserID = BlockedUserID;
            //Po zablokowaniu użytkownika także go przestaje obserwować
            return RedirectToAction("UnFollow", new { FollowedUserID, Information, Site });
        }

        //Obserowanie dane użytkownika
        public async Task<IActionResult> Follow(string FollowedUserID, string Information, string Site)
        {
            UserEntity userToFollow = _context.Users.FirstOrDefault(user => user.Id.Equals(FollowedUserID));
            UserEntity followingUser = _context.Users.FirstOrDefault(user => user.Id.Equals(_userManager.GetUserId(User)));
            FollowUserEntity followedUser = new FollowUserEntity(followingUser, userToFollow);
            _context.Add(followedUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Redirecting", new { Information, Site });
        }

        //Zaprzestanie obserwowania danego użytkownika
        public async Task<IActionResult> UnFollow(string FollowedUserID, string Information, string Site)
        {
            var userToUnfollow = _context.FollowUsers
                .Include(user => user.FollowedUser)
                .Include(user => user.FollowingUser)
                .Where(user => user.FollowedUser.Id.Equals(FollowedUserID) && user.FollowingUser.Id.Equals(_userManager.GetUserId(User)))
                .FirstOrDefault();
            _context.FollowUsers.Remove(userToUnfollow);
            await _context.SaveChangesAsync();
            return RedirectToAction("Redirecting", new { Information, Site });
        }

        //Dodawanie komentarzy
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
            string Site = Request.Form["site"];
            string Information = Request.Form["information"];
            return RedirectToAction("Redirecting", new { Information, Site });
        }

        //Dynamiczne pokazywanie tytułów w wyszukiwarce
        public async Task<IActionResult> LivePostSearch(string search)
        {
            List<PostEntity> res = _context.Posts
               .Where(p => p.Title.Contains(search))
               .ToList();
            return PartialView("LivePostSearch", res);
        }

        //Routing dla naszych view
        public async Task<IActionResult> Redirecting(string Information, string Site) 
        {
            if (Site == "Followed")
            {
                return RedirectToAction("Followed");
            }
            else if (Site == "Index")
            {
                return RedirectToAction("Index", new { Information });

            }
            else if (Site == "ShowPost")
            {
                int Id = int.Parse(Information);
                return RedirectToAction("ShowPost", new { Id });

            }
            else if (Site == "FindedPosts")
            {
                string liveSearchTitle = Information;
                return RedirectToAction("FindedPosts", new { liveSearchTitle });
            }
            else
            {
                return RedirectToAction("Index", new { Information });
            }
        }

        //Funkcja do blokowania użytkownika
        public List<string> BlockedEntities()
        {

            var blockedUsers = _context.BlockedUsers
                    .Where(entry => entry.BlockingUser.Id == _userManager.GetUserId(User))
                    .Select(entry => entry.BlockedUser.Id)
                    .ToList();
            return blockedUsers;
        }

        //Wyszukuje posty poprzez kategorie
        public List<PostEntity> PostsByCategories(string CategoryName)
        {
            var posts = _context.Posts
               .Include(p => p.AuthorUser)
               .Include(p => p.Categories)
               .Where(post => post.Categories.Any(category => category.CategoryName.Equals(CategoryName)))
               .OrderByDescending(post => post.CreatedDate)
               .ToList();
            return posts;
        }

        //Wyszukuje posty poprzez szukany tytuł
        public List<PostEntity> PostsByContain(string search)
        {
            List<PostEntity> posts = _context.Posts
                .Include(p => p.AuthorUser)
                .Where(p => p.Title.Contains(search))
                .ToList();
            return posts;
        }

        //Zwraca wszystkie komentarze

        public List<CommentEntity> GetAllComments()
        {
            List<CommentEntity> comments = _context.Comments
                .Include(comment => comment.AuthorUser)
                .Include(comment => comment.CommentedPost)
                .ToList();
            return comments;
        }
    }
}