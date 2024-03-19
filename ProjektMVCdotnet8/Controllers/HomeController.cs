using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Areas.Identity.Pages.Account;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity; //do sprawdzenia uzytkowknika
using Microsoft.AspNetCore.Mvc; //do sprawdzenia uzytkowknika
using System.Drawing.Printing; 

namespace ProjektMVCdotnet8.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPasswordHasher<UserEntity> has = new PasswordHasher<UserEntity> ();

        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<UserEntity> _userManager;//do sprawdzenia uzytkowknika


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<UserEntity> userManager)
        {
            _userManager = userManager;//do sprawdzenia uzytkowknika
            _logger = logger;
            _context = context;
            CreateElements();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult AddPost()
        {
            List<CategoryEntity> nameCategory = new List<CategoryEntity>();
            foreach (var el in _context.Categories)
            {
                nameCategory.Add(el);
            }

            ViewBag.nameCategory = nameCategory;
            return View();
        }
        public IActionResult PasswordRecovery()
        {
            return View();
        }
        public IActionResult ThxForContact(ContactModel model)
        {
            return View("ThxForContact", model);
        }
        /*        public IActionResult Chat()
                {
                    return View();
                }*/

        public async Task<IActionResult> Chat()
        {
            var user = await _userManager.GetUserAsync(User);//do sprawdzenia uzytkowknika
            //Console.WriteLine("USER: "+user); // wypisuje usera w konsolu
            ViewBag.CurrentUser = user;
            return View(await _context.Messages.ToListAsync());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async void CreateElements()
        {
            if (_context.Users.IsNullOrEmpty())
            {
                UserEntity userEntity = new UserEntity();
                userEntity.Email= "test@user";
                userEntity.UserName = "testuser";
                userEntity.NormalizedUserName = "TESTUSER";
                userEntity.PasswordHash = "AQAAAAIAAYagAAAAEN5tTq6y4IMh2zyfDDricM7Ln3G6JYDvnYNJOeDL3n8K/wpvu1d6lbiEEAXwk/SYnw==";
                userEntity.Nick = "testuser";

                _context.Users.Add(userEntity);

                userEntity = new UserEntity();
                userEntity.Email = "test2@user";
                userEntity.UserName = "test2user";
                userEntity.NormalizedUserName = "TEST2USER";
                userEntity.Nick = "test2user";
                var hashedPassword = has.HashPassword(userEntity, "zaq1@WSX");
                userEntity.PasswordHash = hashedPassword;
                _context.Users.Add(userEntity);


                userEntity = new UserEntity();
                userEntity.Email = "test3@user";
                userEntity.UserName = "test3user";
                userEntity.NormalizedUserName = "TEST3USER";
                userEntity.Nick = "test3user";
                var hashedPassword3 = has.HashPassword(userEntity, "zaq1@WSX");
                userEntity.PasswordHash = hashedPassword3;
                _context.Users.Add(userEntity);

                _context.SaveChanges();
            }
            if (_context.Categories.IsNullOrEmpty()) //Sprawdza czy tablica jest pusta, je¿eli tak to tworzy elementy do tablicy
            {
                if (ModelState.IsValid)
                {
                    CategoryEntity category = new CategoryEntity("Elektronika");
                    _context.Add(category);

                    category = new CategoryEntity("Programowanie");
                    _context.Add(category);

                    category = new CategoryEntity("Komputery");
                    _context.Add(category);

                    category = new CategoryEntity("Sieci");
                    _context.Add(category);

                    category = new CategoryEntity("Spawanie");
                    _context.Add(category);

                    category = new CategoryEntity("Elektryka");
                    _context.Add(category);
                    _context.SaveChanges();
                }
            }
            if (_context.Posts.IsNullOrEmpty())
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == "test@user");
                var categories = _context.Categories.Where(x => x.CategoryName.Equals("Programowanie")).ToList();
                string AttachmentSource = ("29a5ddb2-5544-4fdd-993c-5139fd04d0e4_WolfAttack5.png");
                PostEntity posts = new PostEntity(user, "Tytul_1", "lorem ipsum w gipsum", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Spawanie") || x.CategoryName.Equals("Sieci")).ToList();
                AttachmentSource = ("26897dea-9740-40b0-a638-24e9e77bd528_image.png");
                posts = new PostEntity(user, "Tytul_2", "Jak to jest być skrybą, dobrze?\r\n\r\nA, wie pan, moim zdaniem to nie ma tak, że dobrze, albo że niedobrze.\r\nGdybym miał powiedzieć, co cenię w życiu najbardziej, powiedziałbym, że ludzi.\r\nLudzi, którzy podali mi pomocną dłoń, kiedy sobie nie radziłem, kiedy byłem sam,\r\ni co ciekawe, to właśnie przypadkowe spotkania wpływają na nasze życie.\r\nChodzi o to, że kiedy wyznaje się pewne wartości, nawet pozornie uniwersalne,\r\nbywa, że nie znajduje się zrozumienia, które by tak rzec, które pomaga się nam rozwijać.\r\nJa miałem szczęście, by tak rzec, ponieważ je znalazłem, i dziękuję życiu!\r\nDziękuję mu; życie to śpiew, życie to taniec, życie to miłość!\r\nWielu ludzi pyta mnie o to samo: ale jak ty to robisz, skąd czerpiesz tę radość? A ja odpowiadam, że to proste!\r\nTo umiłowanie życia. To właśnie ono sprawia, że dzisiaj na przykład buduję maszyny, a jutro – kto wie? Dlaczego by nie – oddam się pracy społecznej i będę, ot, choćby, sadzić... doć— m-marchew...\r\n\r\n", AttachmentSource, categories);
                _context.Add(posts);



                categories = _context.Categories.Where(x => x.CategoryName.Equals("Elektryka") || x.CategoryName.Equals("Elektronika")).ToList();
                AttachmentSource = ("2ab0efcf-c566-4996-9220-77f2d948ddb2_OIP.jfif");
                posts = new PostEntity(user, "Tytul_2", "Once upon a time, in a mystical realm known as Racoonia, a curious raccoon named Rolo found himself inexplicably transported from his cozy forest home to a parallel world. This world was unlike anything he had ever seen—a blend of ancient history, magic, and fantastical creatures.\r\n\r\nRolo’s Isekai Adventures: Chronicles of the Raccoon Sage\r\n\r\nThe Great Acorn Quest:\r\nRolo stumbled upon a prophecy etched into the bark of an ancient oak tree. It foretold that only a raccoon with a heart as brave as a squirrel’s would be able to retrieve the legendary Golden Acorn. This acorn, said to grant immense wisdom and power, was hidden deep within the Enchanted Forest.\r\nArmed with determination and a trusty acorn-shaped backpack, Rolo embarked on a perilous journey. Along the way, he encountered talking owls, mischievous sprites, and grumpy trolls—all of whom tested his wit and courage.\r\nThe Council of Creatures:\r\nIn the heart of Racoonia stood the majestic Tree of Ages, where representatives from various species convened. Rolo attended the Council of Creatures, where he met the wise Sage Squirrel and the elusive Fox Oracle.\r\nTogether, they discussed matters of magical balance, negotiated treaties between squirrels and chipmunks, and debated whether the neighboring Bunny Kingdom should be allowed to annex the carrot fields.\r\nThe Timeless Library:\r\nRolo discovered the Library of Lost Leaves, a place where forgotten stories and ancient scrolls resided. Here, he pored over dusty tomes, learning about the legendary raccoons of old—the ones who could transform into shadows, steal moonlight, and brew potions from dewdrops.\r\nHis favorite tale was that of Bandit the Clever, who outwitted a dragon by convincing it that shiny gems were mere pebbles.\r\nThe Moonlit Masquerade:\r\nEvery century, Racoonia hosted the grand Moonlit Masquerade, a magical ball where creatures from all corners of the realm gathered. Rolo donned a tiny tuxedo, complete with a miniature top hat, and danced with a graceful Fairy Fawn.\r\nAs the moon wove its silver threads, Rolo whispered secrets to the Starfire Beetles, who promised to light his way home.\r\nThe Portal’s Whispers:\r\nRolo’s ultimate quest was to find the", AttachmentSource, categories);
                _context.Add(posts);



                categories = _context.Categories.Where(x => x.CategoryName.Equals("Sieci") || x.CategoryName.Equals("Komputery")).ToList();
                AttachmentSource = ("_dd417104-4aca-4b42-a483-d8c85bb48d3b.jfif");
                posts = new PostEntity(user, "Lisa Al Gaib, Prince of Arrakis", "In the mystical world of Racoonia, where shifting sands whispered secrets and ancient prophecies danced on the wind, there existed a peculiar raccoon named Lisa al-Gaib. Her story was woven into the very fabric of the desert planet Arrakis, a place where sandstorms painted the skies and hidden wonders lay beneath the dunes.\r\n\r\nThe Chronicles of Lisa al-Gaib: Raccoon Messiah\r\n1. The Whispering Sands:\r\nLisa al-Gaib was no ordinary raccoon. Born under the crescent moon, her fur bore the hues of sunsets—amber, russet, and gold. The Fremen, native inhabitants of Arrakis, believed her to be the Lisan al-Gaib—the “Voice from the Outer World.”\r\nThe prophecy spoke of a savior, an outsider destined to lead the Fremen to salvation. Lisa’s masked eyes held ancient wisdom, and her paws traced constellations in the shifting sands.\r\n2. The Acorn of Destiny:\r\nOne scorching day, Lisa stumbled upon an acorn unlike any other. It glimmered with starlight, and its whispers echoed through her dreams. The Fremen gathered, chanting her name: “Lisan al-Gaib!”\r\nGuided by the acorn’s glow, Lisa embarked on a quest. She climbed dunes, crossed salt flats, and danced with mirages. Each step revealed a fragment of her purpose.\r\n3. The Trials of the Dunes:\r\nLisa faced trials that tested her raccoon heart. She outwitted sandworms, negotiated with spice traders, and brewed potions from cactus blooms. Her tail flicked like a compass, pointing toward destiny.\r\nStilgar, a grizzled Fremen leader, watched her. His eyes held reverence and doubt. “Is she truly the Lisan al-Gaib?” he wondered.\r\n4. The Bene Gesserit’s Web:\r\nThe Bene Gesserit, a sisterhood of powerful beings, wove their machinations. Lady Jessica, Lisa’s mother, whispered secrets in the wind. “Your daughter is the One,” she told Stilgar.\r\nLisa’s paws traced the ancient glyphs of the Portal Oak, the gateway between worlds. To prove her messianic status, she must perform feats: steal moonlight, balance on a single paw, and recite riddles to sandstorms.\r\n5. The Moonlit Masquerade:\r\nAt the grand Moonlit Masquerade, Lisa donned a cloak of stardust. She danced with Stilgar, who marveled at her every move. “The Lisan al-Gaib!” he exclaimed, mistaking her twirl for cosmic magic.\r\nThe stars whispered, “You are the bridge between realms, Lisa. Your raccoon heart holds the universe.”\r\n6. The Final Glyph:\r\nBeneath the Portal Oak, Lisa faced her ultimate trial. The desert winds carried riddles:\r\n“What has roots as deep as the dunes?”\r\n“What blooms when hope fades?”\r\n“What is the truest treasure?”\r\nLisa answered: “Friendship, resilience, and acorns.”\r\nThe portal shimmered. Lisa stepped through, her tail trailing stardust.\r\nAnd so, Lisa al-Gaib became the raccoon messiah—the Voice from the Outer World. Her legend echoed across Arrakis, whispered by sandstorms and etched into the hearts of Fremen. Whether she returned to her forest home or remained among the dunes, her pawprints remained—a constellation of hope.", AttachmentSource, categories);
                _context.Add(posts);

                _context.SaveChanges();
            }
            //contact init jak puste
            if (_context.ContactForms.IsNullOrEmpty())
            {
                if (ModelState.IsValid)
                {
                    ContactEntity contact = new ContactEntity();
                    contact.Email = "joe@mama.com";
                    contact.Topic = "Nie jestem moderatorem a widze panel moderatora";
                    contact.ContactType = "Znalazłem błąd";
                    contact.ContactContent = "Wydaję mi się że to trochę niebezpieczne ale bede korzystał póki mogę";
                    contact.ContactDate = DateTime.Now;
                    _context.Add(contact);

                    contact = new ContactEntity();
                    contact.Email = "niewidomyMaciek@onet.pl";
                    contact.Topic = "Nic nie widzę";
                    contact.ContactType = "Strona nie działa";
                    contact.ContactContent = "Wchodzę na stronę a tu ciemno, dobrze że mogę kliknąć w formularz kontaktowy";
                    contact.ContactDate = DateTime.Now;
                    _context.Add(contact);

                    contact = new ContactEntity();
                    contact.Email = "WrumWruom@bmw.pl";
                    contact.Topic = "Powinniście dodać coś o samochodach";
                    contact.ContactType = "Propozycja zmian";
                    contact.ContactContent = "Uwielbiam wasze forum ale przydała by się kategoria motoryzacja muszę zapytać się o poradę jak wstwić nowy silnik do mojego 30 letniego bmw bo stary niestety uległ awari";
                    contact.ContactDate = DateTime.Now;
                    _context.Add(contact);

                    contact = new ContactEntity();
                    contact.Email = "GrzegorzHejter@wp.pl";
                    contact.Topic = "Obrzydliwa czcionka";
                    contact.ContactType = "Chciałbym podzielić się swoją opinią";
                    contact.ContactContent = "Uważam że wasza strona powinna zmienić czcionkę na comic sans jest taka mięciutka i miła w czytaniu a to co jest teraz to skandal";
                    contact.ContactDate = DateTime.Now;
                    _context.Add(contact);

                    contact = new ContactEntity();
                    contact.Email = "cojarobietutaj@sanah.kabanosy";
                    contact.Topic = "Zastanawialiście się kiedyś jak samkuje drewno";
                    contact.ContactType = "Inne";
                    contact.ContactContent = "No właśnie ja też nie";
                    contact.ContactDate = DateTime.Now;
                    _context.Add(contact);

                    _context.SaveChanges();
                }
            }
            //chat + wiadomość init jak puste
            if (_context.Chats.IsNullOrEmpty())
            {
                var chatEntity = new ChatEntity();
                var user = _context.Users.FirstOrDefault(u => u.Email == "test@user");// tu zmieniajcie mail
                var user2 = _context.Users.FirstOrDefault(u => u.Email == "test2@user");// tu zmieniajcie mail
                chatEntity.ChattingUser1 = user;
                chatEntity.ChattingUser2 = user2;
                _context.Add(chatEntity);

                MessageEntity messageEntity = new MessageEntity();
                messageEntity.MessageContent = "Rozpoczęcie chatu ";
                messageEntity.UsedChat = chatEntity;
                messageEntity.UsingUser = user;
                messageEntity.SendDate = DateTime.Now;
                _context.Add(messageEntity);

                var chatEntity2 = new ChatEntity();
                var user21 = _context.Users.FirstOrDefault(u => u.Email == "test@user");// tu zmieniajcie mail
                var user22 = _context.Users.FirstOrDefault(u => u.Email == "test3@user");// tu zmieniajcie mail
                chatEntity2.ChattingUser1 = user21;
                chatEntity2.ChattingUser2 = user22;
                _context.Add(chatEntity2);

                _context.SaveChanges();
            }
        }
    }
}
