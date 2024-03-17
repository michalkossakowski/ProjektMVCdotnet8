using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Models;
using System.Diagnostics;

namespace ProjektMVCdotnet8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
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
                userEntity.Email="WERYKTEST@PL";
                userEntity.Nick = "WerykSon";
                userEntity.UserName = "Weryk";
                userEntity.PasswordHash = "ZAQ!2wsx";
                _context.Add(userEntity);
                await _context.SaveChangesAsync();
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
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.Posts.IsNullOrEmpty())
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == "WERYKTEST@PL");
                var categories = _context.Categories.Where(x => x.CategoryName.Equals("Programowanie")).ToList();
                string AttachmentSource = ("29a5ddb2-5544-4fdd-993c-5139fd04d0e4_WolfAttack5.png");
                PostEntity posts = new PostEntity(user, "Tytul_1", "lorem ipsum w gipsum", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Spawanie") || x.CategoryName.Equals("Sieci")).ToList();
                AttachmentSource = ("26897dea-9740-40b0-a638-24e9e77bd528_image.png");
                posts = new PostEntity(user, "Tytul_2", "Jak to jest byæ skryb¹, dobrze?\r\n\r\nA, wie pan, moim zdaniem to nie ma tak, ¿e dobrze, albo ¿e niedobrze.\r\nGdybym mia³ powiedzieæ, co ceniê w ¿yciu najbardziej, powiedzia³bym, ¿e ludzi.\r\nLudzi, którzy podali mi pomocn¹ d³oñ, kiedy sobie nie radzi³em, kiedy by³em sam,\r\ni co ciekawe, to w³aœnie przypadkowe spotkania wp³ywaj¹ na nasze ¿ycie.\r\nChodzi o to, ¿e kiedy wyznaje siê pewne wartoœci, nawet pozornie uniwersalne,\r\nbywa, ¿e nie znajduje siê zrozumienia, które by tak rzec, które pomaga siê nam rozwijaæ.\r\nJa mia³em szczêœcie, by tak rzec, poniewa¿ je znalaz³em, i dziêkujê ¿yciu!\r\nDziêkujê mu; ¿ycie to œpiew, ¿ycie to taniec, ¿ycie to mi³oœæ!\r\nWielu ludzi pyta mnie o to samo: ale jak ty to robisz, sk¹d czerpiesz tê radoœæ? A ja odpowiadam, ¿e to proste!\r\nTo umi³owanie ¿ycia. To w³aœnie ono sprawia, ¿e dzisiaj na przyk³ad budujê maszyny, a jutro – kto wie? Dlaczego by nie – oddam siê pracy spo³ecznej i bêdê, ot, choæby, sadziæ... doæ— m-marchew...\r\n\r\nTekst pochodzi z", AttachmentSource, categories);
                _context.Add(posts);



                categories = _context.Categories.Where(x => x.CategoryName.Equals("Elektryka") || x.CategoryName.Equals("Elektronika")).ToList();
                AttachmentSource = ("2ab0efcf-c566-4996-9220-77f2d948ddb2_OIP.jfif");
                posts = new PostEntity(user, "Tytul_2", "Once upon a time, in a mystical realm known as Racoonia, a curious raccoon named Rolo found himself inexplicably transported from his cozy forest home to a parallel world. This world was unlike anything he had ever seen—a blend of ancient history, magic, and fantastical creatures.\r\n\r\nRolo’s Isekai Adventures: Chronicles of the Raccoon Sage\r\n\r\nThe Great Acorn Quest:\r\nRolo stumbled upon a prophecy etched into the bark of an ancient oak tree. It foretold that only a raccoon with a heart as brave as a squirrel’s would be able to retrieve the legendary Golden Acorn. This acorn, said to grant immense wisdom and power, was hidden deep within the Enchanted Forest.\r\nArmed with determination and a trusty acorn-shaped backpack, Rolo embarked on a perilous journey. Along the way, he encountered talking owls, mischievous sprites, and grumpy trolls—all of whom tested his wit and courage.\r\nThe Council of Creatures:\r\nIn the heart of Racoonia stood the majestic Tree of Ages, where representatives from various species convened. Rolo attended the Council of Creatures, where he met the wise Sage Squirrel and the elusive Fox Oracle.\r\nTogether, they discussed matters of magical balance, negotiated treaties between squirrels and chipmunks, and debated whether the neighboring Bunny Kingdom should be allowed to annex the carrot fields.\r\nThe Timeless Library:\r\nRolo discovered the Library of Lost Leaves, a place where forgotten stories and ancient scrolls resided. Here, he pored over dusty tomes, learning about the legendary raccoons of old—the ones who could transform into shadows, steal moonlight, and brew potions from dewdrops.\r\nHis favorite tale was that of Bandit the Clever, who outwitted a dragon by convincing it that shiny gems were mere pebbles.\r\nThe Moonlit Masquerade:\r\nEvery century, Racoonia hosted the grand Moonlit Masquerade, a magical ball where creatures from all corners of the realm gathered. Rolo donned a tiny tuxedo, complete with a miniature top hat, and danced with a graceful Fairy Fawn.\r\nAs the moon wove its silver threads, Rolo whispered secrets to the Starfire Beetles, who promised to light his way home.\r\nThe Portal’s Whispers:\r\nRolo’s ultimate quest was to find the", AttachmentSource, categories);
                _context.Add(posts);



                categories = _context.Categories.Where(x => x.CategoryName.Equals("Sieci") || x.CategoryName.Equals("Komputery")).ToList();
                AttachmentSource = ("_dd417104-4aca-4b42-a483-d8c85bb48d3b.jfif");
                posts = new PostEntity(user, "Lisa Al Gaib, Prince of Arrakis", "In the mystical world of Racoonia, where shifting sands whispered secrets and ancient prophecies danced on the wind, there existed a peculiar raccoon named Lisa al-Gaib. Her story was woven into the very fabric of the desert planet Arrakis, a place where sandstorms painted the skies and hidden wonders lay beneath the dunes.\r\n\r\nThe Chronicles of Lisa al-Gaib: Raccoon Messiah\r\n1. The Whispering Sands:\r\nLisa al-Gaib was no ordinary raccoon. Born under the crescent moon, her fur bore the hues of sunsets—amber, russet, and gold. The Fremen, native inhabitants of Arrakis, believed her to be the Lisan al-Gaib—the “Voice from the Outer World.”\r\nThe prophecy spoke of a savior, an outsider destined to lead the Fremen to salvation. Lisa’s masked eyes held ancient wisdom, and her paws traced constellations in the shifting sands.\r\n2. The Acorn of Destiny:\r\nOne scorching day, Lisa stumbled upon an acorn unlike any other. It glimmered with starlight, and its whispers echoed through her dreams. The Fremen gathered, chanting her name: “Lisan al-Gaib!”\r\nGuided by the acorn’s glow, Lisa embarked on a quest. She climbed dunes, crossed salt flats, and danced with mirages. Each step revealed a fragment of her purpose.\r\n3. The Trials of the Dunes:\r\nLisa faced trials that tested her raccoon heart. She outwitted sandworms, negotiated with spice traders, and brewed potions from cactus blooms. Her tail flicked like a compass, pointing toward destiny.\r\nStilgar, a grizzled Fremen leader, watched her. His eyes held reverence and doubt. “Is she truly the Lisan al-Gaib?” he wondered.\r\n4. The Bene Gesserit’s Web:\r\nThe Bene Gesserit, a sisterhood of powerful beings, wove their machinations. Lady Jessica, Lisa’s mother, whispered secrets in the wind. “Your daughter is the One,” she told Stilgar.\r\nLisa’s paws traced the ancient glyphs of the Portal Oak, the gateway between worlds. To prove her messianic status, she must perform feats: steal moonlight, balance on a single paw, and recite riddles to sandstorms.\r\n5. The Moonlit Masquerade:\r\nAt the grand Moonlit Masquerade, Lisa donned a cloak of stardust. She danced with Stilgar, who marveled at her every move. “The Lisan al-Gaib!” he exclaimed, mistaking her twirl for cosmic magic.\r\nThe stars whispered, “You are the bridge between realms, Lisa. Your raccoon heart holds the universe.”\r\n6. The Final Glyph:\r\nBeneath the Portal Oak, Lisa faced her ultimate trial. The desert winds carried riddles:\r\n“What has roots as deep as the dunes?”\r\n“What blooms when hope fades?”\r\n“What is the truest treasure?”\r\nLisa answered: “Friendship, resilience, and acorns.”\r\nThe portal shimmered. Lisa stepped through, her tail trailing stardust.\r\nAnd so, Lisa al-Gaib became the raccoon messiah—the Voice from the Outer World. Her legend echoed across Arrakis, whispered by sandstorms and etched into the hearts of Fremen. Whether she returned to her forest home or remained among the dunes, her pawprints remained—a constellation of hope.", AttachmentSource, categories);
                _context.Add(posts);

                await _context.SaveChangesAsync();
            }
        }

    }
}
