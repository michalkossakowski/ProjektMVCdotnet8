using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;
using ProjektMVCdotnet8.Models;
using ProjektMVCdotnet8.Repository;
namespace ProjektMVCdotnet8.Controllers
{
    public class PostController : Controller
    {
        private readonly IFollowUserRepository _followUserRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IBlockedUserRepository _blockedUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        public PostController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IPostRepository postRepository, IFollowUserRepository followUserRepository, ICommentRepository commentRepository, IBlockedUserRepository blockedUserRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _followUserRepository = followUserRepository;
            _blockedUserRepository = blockedUserRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        // Wyświetla strone z danymi postami na podstawie kategorii. Odfiltruje posty blokowanych użytkowników
        public async Task<IActionResult> Index(string Information, string site)
        {
            //Przesyła informacje jakie posty będzie wyświetlał na stronie według kategorii
            TempData["Information"] = Information;
            TempData["Site"] = "Index";

            IEnumerable<PostEntity> posts = await _postRepository.GetByCategory(Information);

            IEnumerable<CommentEntity> comments = await _commentRepository.GetAll();
            ViewBag.Comments = comments;

            if (_signInManager.IsSignedIn(User))
            {
                var blockedUsers = await _blockedUserRepository.GetAllIDBlockedBy(_userManager.GetUserId(User));
                var filteredPosts = posts
                    .Where(post => !blockedUsers.Contains(post.AuthorUser.Id))
                    .ToList();

                var followedUsers = await _followUserRepository.GetAllFollowedBY(_userManager.GetUserId(User));
                TempData["FollowedUsers"] = followedUsers.Select(user => user.Id).ToList();
                return View("Index", filteredPosts);
            }
            else
            {
                TempData["FollowedUsers"] = "null";
                return View("Index", posts);
            }
        }

        // Wyświetla strone z postami obserwowanych użytkowników. Odfiltruje posty blokowanych użytkowników
        public async Task<IActionResult> Followed()
        {
            TempData["Site"] = "Followed";

            IEnumerable<CommentEntity> comments = await _commentRepository.GetAll();
            ViewBag.Comments = comments;

            var followedUsers = await _followUserRepository.GetAllFollowedBY(_userManager.GetUserId(User));
            var followedToString = followedUsers.Select(user => user.Id).ToList();
            TempData["FollowedUsers"] = followedToString;

            var posts = await _postRepository.GetAll();
            var filteredPosts = posts
                .Where(post => followedToString.Contains(post.AuthorUser.Id))
                .ToList();
            return View("Index", filteredPosts);
        }

        // Wyświetla strone z postami pasującymi do lokalizacji zalogowanego użytkownika. Odfiltruje posty blokowanych użytkowników
        public async Task<IActionResult> Local(string? Information, string site)
        {
            TempData["Information"] = Information;
            TempData["Site"] = "Local";

            IEnumerable<PostEntity> posts = await _postRepository.GetByCity(Information, await _userManager.GetUserAsync(User));

            // dodatkowy filtr biorący tylko te, które są oznaczone jako Lokalne posty :D
            posts = posts.Where(post => post.isLocal).ToList();

            IEnumerable<CommentEntity> comments = await _commentRepository.GetAll();
            ViewBag.Comments = comments;

            if (_signInManager.IsSignedIn(User))
            {
                var blockedUsers = await _blockedUserRepository.GetAllIDBlockedBy(_userManager.GetUserId(User));
                var filteredPosts = posts
                    .Where(post => !blockedUsers.Contains(post.AuthorUser.Id))
                    .ToList();

                var followedUsers = await _followUserRepository.GetAllFollowedBY(_userManager.GetUserId(User));
                TempData["FollowedUsers"] = followedUsers.Select(user => user.Id).ToList();
                return View("Local", filteredPosts);
            }
            else
            {
                TempData["FollowedUsers"] = "null";
                return View("Local", posts);
            }
        }

        // Zwraca widok z pojędyńczym postem
        public async Task<IActionResult> ShowPost(int Id)
        {
            TempData["Site"] = "ShowPost";
            TempData["Information"] = Id.ToString();

            IEnumerable<CommentEntity> comments = await _commentRepository.GetByPostID(Id);
            ViewBag.Comments = comments;

            var post = await _postRepository.GetById(Id);

            var followedUsers = await _followUserRepository.GetByIdFollowedUser(post.AuthorUser.Id, _userManager.GetUserId(User));
            if (followedUsers is null)
            {
                TempData["FollowedUsers"] = "";
            }
            else
            {
                TempData["FollowedUsers"] = followedUsers.Id;
            }
            return View("ShowPost", post);
        }

        // Zwraca widok z postami które wyszukujemy przez wyszukiwarke
        public async Task<IActionResult> FindedPosts(string liveSearchTitle)
        {
            TempData["Information"] = liveSearchTitle;
            TempData["Site"] = "FindedPosts";

            IEnumerable<CommentEntity> comments = await _commentRepository.GetAll();
            ViewBag.Comments = comments;


            var posts = await _postRepository.GetByContain(liveSearchTitle).ConfigureAwait(false);

            if (_signInManager.IsSignedIn(User))
            {
                var blockedUsers = await _blockedUserRepository.GetAllIDBlockedBy(_userManager.GetUserId(User));
                var filteredPosts = posts
                    .Where(post => !blockedUsers.Contains(post.AuthorUser.Id))
                    .ToList();

                var followedUsers = await _followUserRepository.GetAllFollowedBY(_userManager.GetUserId(User));
                TempData["FollowedUsers"] = followedUsers.Select(user => user.Id).ToList();
                return View("Index", filteredPosts);
            }
            else
            {
                TempData["FollowedUsers"] = "null";
                return View("Index", posts);
            }
        }


        // Blokuje danego użytkownika
        public async Task<IActionResult> Block(string BlockedUserID, string Information, string Site)
        {
            UserEntity userToBlock = await _userRepository.GetUserByID(BlockedUserID);
            UserEntity blockingUser = await _userRepository.GetUserByID(_userManager.GetUserId(User));
            BlockedUserEntity blockedUser = new BlockedUserEntity(blockingUser, userToBlock);
            _blockedUserRepository.Add(blockedUser);

            string FollowedUserID = BlockedUserID;

            // Po zablokowaniu użytkownika także go przestaje obserwować
            return RedirectToAction("UnFollow", new { FollowedUserID, Information, Site });
        }

        // Obserowanie dane użytkownika
        public async Task<IActionResult> Follow(string FollowedUserID, string Information, string Site)
        {
            UserEntity userToFollow = await _userRepository.GetUserByID(FollowedUserID);
            UserEntity followingUser =  await _userRepository.GetUserByID(_userManager.GetUserId(User));
            FollowUserEntity followedUser = new FollowUserEntity(followingUser, userToFollow);
            _followUserRepository.Add(followedUser);
            return RedirectToAction("Redirecting", new { Information, Site });
        }

        // Zaprzestanie obserwowania danego użytkownika
        public async Task<IActionResult> UnFollow(string FollowedUserID, string Information, string Site)
        {
            var userToUnfollow = await _followUserRepository.GetById(FollowedUserID, _userManager.GetUserId(User));
            if (userToUnfollow is not null)
            {
                _followUserRepository.Delete(userToUnfollow);
            }
            return RedirectToAction("Redirecting", new { Information, Site });
        }

        // Dodawanie komentarzy
        public async Task<IActionResult> AddComment(CommentEntity commentEntity)
        {
            var user = await _userManager.GetUserAsync(User);
            var post = await _postRepository.GetById(int.Parse(Request.Form["postId"]));
            commentEntity = await _commentRepository.MapCommentEntity(commentEntity, user, post, _userManager.GetUserName(User));
            _commentRepository.Add(commentEntity);

            string Site = Request.Form["Site"];
            string Information = Request.Form["Information"];
            return RedirectToAction("Redirecting", new { Information, Site });
        }
        // Dynamiczne pokazywanie tytułów w wyszukiwarce
        public async Task<IActionResult> LivePostSearch(string search)
        {

            IEnumerable<PostEntity> posts = await _postRepository.GetByContain(search);

            if (_signInManager.IsSignedIn(User))
            {
                var blockedUsers = await _blockedUserRepository.GetAllIDBlockedBy(_userManager.GetUserId(User));
                var filteredPosts = posts
                    .Where(post => !blockedUsers.Contains(post.AuthorUser.Id))
                    .ToList();

                var followedUsers = await _followUserRepository.GetAllFollowedBY(_userManager.GetUserId(User));
                TempData["FollowedUsers"] = followedUsers.Select(user => user.Id).ToList();
                return PartialView("LivePostSearch", filteredPosts);
            }
            else
            {
                TempData["FollowedUsers"] = "null";
                return PartialView("LivePostSearch", posts);
            }
        }

        // Routing dla naszych view. Information zawiera informacje takie np nazwa kategorii, czy wartość wpisana w wyszukiwarce.
        // Site informuje do jakiej akcji (strony) ma przekierować.
        public async Task<IActionResult> Redirecting(string Information, string Site)
        {
            if (Site == "Followed")
            {
                return RedirectToAction("Followed");
            }
            else if (Site == "Local")
            {
                return RedirectToAction("Local");
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
            else if (Site == "Home")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> CreatePost([Bind("Id,Title,PostContent,AttachmentSource,Categories,isLocal")] PostModel postModel)
        {
            var loggedUser = await _userManager.GetUserAsync(User);

            PostEntity postEntity = new PostEntity();
            postEntity.Title = postModel.Title;
            postEntity.PostContent = postModel.PostContent;
            postEntity.AuthorUser = loggedUser;
            postEntity.CreatedDate = DateTime.Now;
            postEntity.isLocal = postModel.isLocal;

            if (postModel.AttachmentSource != null && postModel.AttachmentSource.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "attachments");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + postModel.AttachmentSource.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await postModel.AttachmentSource.CopyToAsync(fileStream);
                }
                Console.WriteLine(uniqueFileName);
                postEntity.AttachmentSource = uniqueFileName;
            }
            else
            {
                postEntity.AttachmentSource = null;
            }
            postEntity.Categories = new List<CategoryEntity>();
            var selectedCategoryIds = Request.Form["SelectedCategories"].ToList();
            foreach (var categoryId in selectedCategoryIds)
            {
                var category = await _categoryRepository.GetById(int.Parse(categoryId));
                if (category != null)
                {
                    postEntity.Categories.Add(category);
                }
            }

            if (postEntity.Categories.Count == 0)
            {
                return RedirectToAction("AddPost", "Home", postModel);
            }


            loggedUser.Points += 1000;
            _postRepository.Add(postEntity);
            //string Information = _context.Categories.FirstOrDefault(c => c.Id == int.Parse(selectedCategoryIds[0])).CategoryName;
            var firstSelectedCategory = await _categoryRepository.GetById(int.Parse(selectedCategoryIds[0]));
            string Information = firstSelectedCategory.CategoryName;
            string site = "Index";
            return RedirectToAction("Index", "Post", new { Information, site });
        }
    }
}