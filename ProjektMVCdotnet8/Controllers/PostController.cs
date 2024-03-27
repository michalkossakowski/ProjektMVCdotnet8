using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;
using ProjektMVCdotnet8.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
namespace ProjektMVCdotnet8.Controllers
{
    public class PostController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IPostRepository _postRepository;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IFollowUserRepository _followUserRepository;
        private readonly UserManager<UserEntity> _userManager;
        public PostController(ApplicationDbContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IPostRepository postRepository, IFollowUserRepository followUserRepository)
        {
            _postRepository = postRepository;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _followUserRepository = followUserRepository;
        }


        public async Task<IActionResult> Index(string Information, string site)
        {
            //Przesyła informacje jakie posty będzie wyświetlał na stronie według kategorii
            TempData["Information"] = Information;
            TempData["Site"] = "Index";

            IEnumerable<PostEntity> posts = await _postRepository.GetByCategory(Information);

            IEnumerable<CommentEntity> comments =await GetAllComments();
            ViewBag.Comments = comments;

            if (_signInManager.IsSignedIn(User))
            {
                var blockedUsers = BlockedEntities();
                var filteredPosts = posts
                    .Where(post => !blockedUsers.Contains(post.AuthorUser.Id))
                    .ToList();

                var followedUsers = await _followUserRepository.GetAllFollowed(_userManager.GetUserId(User));
                TempData["FollowedUsers"] = followedUsers.Select(user => user.Id).ToList();
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

            IEnumerable<CommentEntity> comments = await GetAllComments();
            ViewBag.Comments = comments;

            var followedUsers = await _followUserRepository.GetAllFollowed(_userManager.GetUserId(User));
            var followedToString = followedUsers.Select(user => user.Id).ToList();
            TempData["FollowedUsers"] = followedToString;

            var posts =  _postRepository.GetAll();
            var filteredPosts = (await posts)
                .Where(post => followedToString.Contains(post.AuthorUser.Id))
                .ToList();
            return View("Index", filteredPosts);
        }
        public async Task<IActionResult> Local(string Information, string site)
        {
            //Przesyła informacje jakie posty będzie wyświetlał na stronie według kategorii
            TempData["Information"] = Information;
            TempData["Site"] = "Index";

            IEnumerable<PostEntity> posts = await _postRepository.GetByCity(Information);

            IEnumerable<CommentEntity> comments = await GetAllComments();
            ViewBag.Comments = comments;

            if (_signInManager.IsSignedIn(User))
            {
                var blockedUsers = BlockedEntities();
                var filteredPosts = posts
                    .Where(post => !blockedUsers.Contains(post.AuthorUser.Id))
                    .ToList();

                var followedUsers = await _followUserRepository.GetAllFollowed(_userManager.GetUserId(User));
                TempData["FollowedUsers"] = followedUsers.Select(user => user.Id).ToList();
                return View("Index", filteredPosts);
            }
            else
            {
                TempData["FollowedUsers"] = "null";
                return View("Index", posts);
            }
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
            
            IEnumerable<CommentEntity> comments = await GetAllComments();
            ViewBag.Comments = comments;


            var posts = await _postRepository.GetByContain(liveSearchTitle).ConfigureAwait(false);

            if (_signInManager.IsSignedIn(User))
            {
                var blockedUsers = BlockedEntities();
                var filteredPosts = posts
                    .Where(post => !blockedUsers.Contains(post.AuthorUser.Id))
                    .ToList();

                var followedUsers = await _followUserRepository.GetAllFollowed(_userManager.GetUserId(User));
                TempData["FollowedUsers"] = followedUsers.Select(user => user.Id).ToList();
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
            if (userToUnfollow is not null)
            {
                _context.FollowUsers.Remove(userToUnfollow);
            }
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
            user.Points += 500;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            string Site = Request.Form["Site"];
            string Information = Request.Form["Information"];
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

        //Zwraca wszystkie komentarze

        public  async Task<IEnumerable<CommentEntity>> GetAllComments()
        {
            var comments = _context.Comments
                .Include(comment => comment.AuthorUser)
                .Include(comment => comment.CommentedPost)
                .ToListAsync();
            return await comments;
        }
    }
}