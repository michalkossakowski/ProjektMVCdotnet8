using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<UserEntity> has = new PasswordHasher<UserEntity>();

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
            if (_userManager.GetUserName(User) is null)
            {
                ViewBag.userPoints = 0;
            }
            else
            {
                var currentUser = _context.Users.FirstOrDefault(u => u.Nick == _userManager.GetUserName(User));
                ViewBag.userPoints = currentUser.Points;
            }
          
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
        public IActionResult ThxForReport()
        {
            return View();
        }
        public IActionResult ModPanel()
        {
            return View();
        }
        public IActionResult Ranking()
        {
            List<(string, int)> lista = new List<(string, int)>();
            foreach (var user in _context.Users)
            {
                lista.Add((user.UserName, (int)user.Points));
            }
            lista.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            ViewBag.UserList = lista;
            return View();
        }
        public IActionResult ChatList()
        {
            var user = _userManager.GetUserName(User);
            var chatList = new List<ChatEntity>();
            List <string> secondUserAvatar = new List<string>();

            foreach (var ch in _context.Chats)
            {
                var u1 = ch.User1Nick;
                var u2 = ch.User2Nick;
                UserEntity secondUser = new UserEntity();
                if (user == u1 || user == u2)
                {
                    if(user == u1)
                    {
                        secondUser = _context.Users.FirstOrDefault(n => n.Nick == u2);
                    }
                    else
                    {
                        secondUser = _context.Users.FirstOrDefault(n => n.Nick == u1);
                    }
                    chatList.Add(ch);
                    secondUserAvatar.Add(secondUser.Avatar);
                }
            }
            ViewBag.chatList = chatList;
            ViewBag.currentUser = user;
            ViewBag.secondUserAvatar = secondUserAvatar;
            return View();
        }

        public async Task<IActionResult> Chat(int chatId)
        {
            var chat = _context.Chats.FirstOrDefault(c => c.Id == chatId);
            ViewBag.CurrentChat = chat.Id;

            var user = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUser = user;

            var user2 = chat.User1Nick;
            if (user.Nick == user2)
            {
                user2 = chat.User2Nick;
            }
            ViewBag.SecondUser = user2;
            string avatar2 = _context.Users.FirstOrDefault(u => u.Nick == user2).Avatar;
            ViewBag.SecondAvatar = avatar2;
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
                userEntity.Email = "test@user";
                userEntity.UserName = "testuser";
                userEntity.NormalizedUserName = "TESTUSER";
                userEntity.PasswordHash = "AQAAAAIAAYagAAAAEN5tTq6y4IMh2zyfDDricM7Ln3G6JYDvnYNJOeDL3n8K/wpvu1d6lbiEEAXwk/SYnw==";
                userEntity.Nick = "testuser";
                userEntity.Avatar = "97c4b8d1-b58c-42d6-97a9-8dad3af404d4_profilowe.png";
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
                userEntity.Avatar = "szop_skywalker.jpg";
                var hashedPassword3 = has.HashPassword(userEntity, "zaq1@WSX");
                userEntity.PasswordHash = hashedPassword3;
                _context.Users.Add(userEntity);

                userEntity = new UserEntity();
                userEntity.Email = "admin@user";
                userEntity.UserName = "admin";
                userEntity.NormalizedUserName = "ADMIN";
                userEntity.Nick = "admin";
                userEntity.Avatar = "szop_toronto.jpg";
                userEntity.PasswordHash = has.HashPassword(userEntity, "a");
                userEntity.Points = 3116;
                _context.Users.Add(userEntity);

                _context.SaveChanges();
            }
            if (_context.Categories.IsNullOrEmpty()) //Sprawdza czy tablica jest pusta, je¿eli tak to tworzy elementy do tablicy
            {
                if (ModelState.IsValid)
                {
                    CategoryEntity category = new CategoryEntity("Elektronika");
                    _context.Add(category);

                    category = new CategoryEntity("Elektryka");
                    _context.Add(category);

                    category = new CategoryEntity("Komputery");
                    _context.Add(category);

                    category = new CategoryEntity("Programowanie");
                    _context.Add(category);

                    category = new CategoryEntity("Sieci");
                    _context.Add(category);

                    category = new CategoryEntity("Spawanie");
                    _context.Add(category);

                    _context.SaveChanges();
                }
            }
            if (_context.Posts.IsNullOrEmpty())
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == "test@user");
                var user1 = _context.Users.FirstOrDefault(u => u.Email == "test2@user");
                var user2 = _context.Users.FirstOrDefault(u => u.Email == "test3@user");

                var categories = _context.Categories.Where(x => x.CategoryName.Equals("Programowanie") || x.CategoryName.Equals("Komputery")).ToList();
                string AttachmentSource = ("szop_toronto.jpg");
                PostEntity posts = new PostEntity(user, "Teraz Rodzina", "Szopinic Toronto", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Spawanie") || x.CategoryName.Equals("Sieci")).ToList();
                AttachmentSource = ("26897dea-9740-40b0-a638-24e9e77bd528_image.png");
                posts = new PostEntity(user, "Dibrze czy nie dobrze?", "Jak to jest być skrybą, dobrze?\r\n\r\nA, wie pan, moim zdaniem to nie ma tak, że dobrze, albo że niedobrze.\r\nGdybym miał powiedzieć, co cenię w życiu najbardziej, powiedziałbym, że ludzi.\r\nLudzi, którzy podali mi pomocną dłoń, kiedy sobie nie radziłem, kiedy byłem sam,\r\ni co ciekawe, to właśnie przypadkowe spotkania wpływają na nasze życie.\r\nChodzi o to, że kiedy wyznaje się pewne wartości, nawet pozornie uniwersalne,\r\nbywa, że nie znajduje się zrozumienia, które by tak rzec, które pomaga się nam rozwijać.\r\nJa miałem szczęście, by tak rzec, ponieważ je znalazłem, i dziękuję życiu!\r\nDziękuję mu; życie to śpiew, życie to taniec, życie to miłość!\r\nWielu ludzi pyta mnie o to samo: ale jak ty to robisz, skąd czerpiesz tę radość? A ja odpowiadam, że to proste!\r\nTo umiłowanie życia. To właśnie ono sprawia, że dzisiaj na przykład buduję maszyny, a jutro – kto wie? Dlaczego by nie – oddam się pracy społecznej i będę, ot, choćby, sadzić... doć— m-marchew...\r\n\r\n", AttachmentSource, categories);
                _context.Add(posts);


                categories = _context.Categories.Where(x => x.CategoryName.Equals("Elektryka") || x.CategoryName.Equals("Elektronika")).ToList();
                AttachmentSource = ("_dd417104-4aca-4b42-a483-d8c85bb48d3b.jfif");
                posts = new PostEntity(user, "Dune", "Once upon a time, in a mystical realm known as Racoonia, a curious raccoon named Rolo found himself inexplicably transported from his cozy forest home to a parallel world. This world was unlike anything he had ever seen—a blend of ancient history, magic, and fantastical creatures.\r\n\r\nRolo’s Isekai Adventures: Chronicles of the Raccoon Sage\r\n\r\nThe Great Acorn Quest:\r\nRolo stumbled upon a prophecy etched into the bark of an ancient oak tree. It foretold that only a raccoon with a heart as brave as a squirrel’s would be able to retrieve the legendary Golden Acorn. This acorn, said to grant immense wisdom and power, was hidden deep within the Enchanted Forest.\r\nArmed with determination and a trusty acorn-shaped backpack, Rolo embarked on a perilous journey. Along the way, he encountered talking owls, mischievous sprites, and grumpy trolls—all of whom tested his wit and courage.\r\nThe Council of Creatures:\r\nIn the heart of Racoonia stood the majestic Tree of Ages, where representatives from various species convened. Rolo attended the Council of Creatures, where he met the wise Sage Squirrel and the elusive Fox Oracle.\r\nTogether, they discussed matters of magical balance, negotiated treaties between squirrels and chipmunks, and debated whether the neighboring Bunny Kingdom should be allowed to annex the carrot fields.\r\nThe Timeless Library:\r\nRolo discovered the Library of Lost Leaves, a place where forgotten stories and ancient scrolls resided. Here, he pored over dusty tomes, learning about the legendary raccoons of old—the ones who could transform into shadows, steal moonlight, and brew potions from dewdrops.\r\nHis favorite tale was that of Bandit the Clever, who outwitted a dragon by convincing it that shiny gems were mere pebbles.\r\nThe Moonlit Masquerade:\r\nEvery century, Racoonia hosted the grand Moonlit Masquerade, a magical ball where creatures from all corners of the realm gathered. Rolo donned a tiny tuxedo, complete with a miniature top hat, and danced with a graceful Fairy Fawn.\r\nAs the moon wove its silver threads, Rolo whispered secrets to the Starfire Beetles, who promised to light his way home.\r\nThe Portal’s Whispers:\r\nRolo’s ultimate quest was to find the", AttachmentSource, categories);
                _context.Add(posts);


                categories = _context.Categories.Where(x => x.CategoryName.Equals("Spawanie") || x.CategoryName.Equals("Elektryka")).ToList();
                AttachmentSource = ("Septic_tank_PL.svg");
                posts = new PostEntity(user1, "Szambonourek Głebinowy", "Pewnego razu w małej wiosce o nazwie Śmierdzące Doliny, mieszkał dzielny chłopiec o imieniu Stefan. Stefan był niezwykle ciekawski i zawsze szukał przygód. Pewnego dnia, gdy słońce świeciło na całe niebo, Stefan postanowił odkryć tajemnicę, która spoczywała za wzgórzem.\r\n\r\nWędrując przez gęsty las, Stefan natknął się na coś, co wydało mu się dziwne. To była stara, zardzewiała pokrywa szamba. Stefan wiedział, że to nie jest zwykła pokrywa. Była zbyt tajemnicza, by ją zignorować.\r\n\r\nPostanowił zdjąć pokrywę i zerknąć do środka. Kiedy otworzył ją, z oczu Stefana wydobył się straszny smród. Ale to nie zniechęciło go. Wszedł do szamba i zobaczył schody prowadzące w dół.\r\n\r\nSchody były wilgotne i śliskie, ale Stefan nie bał się. Zszedł coraz niżej, aż dotarł do podziemnej krainy szamba. Tam spotkał Szamboluda, dziwną postać o zielonej skórze i śmierdzących włosach.\r\n\r\nSzambolud opowiedział Stefanowi historię swojego świata. Okazało się, że pod ziemią istniała cała społeczność szambowców. Żyli w tunelach i korytarzach, korzystając z resztek ludzkich rzeczy, które trafiały do szamba.\r\n\r\nStefan spędził wiele dni w Śmierdzących Dolinach, poznając zwyczaje i obyczaje szambowców. Dowiedział się, że ich największym skarbem były stare buty, zepsute zabawki i zapomniane książki. Szambowcy byli niezwykle pracowici i dbali o swoje podziemne królestwo.\r\n\r\nStefan stał się przyjacielem Szamboluda i pomagał mu w codziennych obowiązkach. Razem naprawiali stare rowery, czyścili zatkane rury i zbierali zapomniane monety. Stefan zrozumiał, że nawet w najbardziej nieprzyjemnych miejscach można znaleźć piękno i przyjaźń.\r\n\r\nI tak oto, dzielny Stefan i Szambolud żyli razem w Śmierdzących Dolinach, dzieląc się swoimi historiami i marzeniami. A każdy, kto przeszedł obok starej pokrywy szamba, nie mógł uwierzyć, że pod ziemią istnieje taka magiczna kraina.\r\n\r\nI tak kończę moją bajkę o szambie. Niech cię nie zniechęcają brud i smród – czasem największe przygody czekają w najmniej spodziewanych miejscach!", AttachmentSource, categories);
                _context.Add(posts);

                user = _context.Users.FirstOrDefault(u => u.Email == "test2@user");
                categories = _context.Categories.Where(x => x.CategoryName.Equals("Programowanie") || x.CategoryName.Equals("Elektryka")).ToList();
                AttachmentSource = ("49626137-41bc-4b33-8707-f5f6573160fc_shopOpen-Photoroom.png");
                posts = new PostEntity(user1, "Sklepikarz", "Exegi monumentum aere perennius\r\nregalique situ pyramidum altius,\r\nquod non imber edax, non aquilo impotens\r\npossit diruere aut innumerabilis\r\nannorum series et fuga temporum.\r\nnon omnis moriar multaque pars mei\r\nvitabit Libitinam; usque ego postera\r\ncrescam laude recens, dum Capitolium\r\nscandet cum tacita virgine pontifex.\r\ndicar, qua violens obstrepit Aufidus\r\net qua pauper aquae Daunus agrestium\r\nregnavit populorum, ex humili potens,\r\nprinceps Aeolium carmen ad Italos\r\ndeduxisse modos. sume superbiam\r\nquaesitam meritis et mihi Delphica\r\nlauro cinge volens, Melpomene, comam.", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Programowanie") || x.CategoryName.Equals("Elektronika")).ToList();
                AttachmentSource = ("_04983ed1-cb43-40b4-802a-55586811b99f.jfif");
                posts = new PostEntity(user2, "I am speed, i am kebab", "Być szybkim kebabem to nie lada wyzwanie! Oto kilka wskazówek dla naszego niezwykłego kebaba, który marzy o byciu najszybszym:\r\n\r\nZwinność: Ćwicz swoje obroty! Wyobraź sobie, że jesteś na obracającej się rusztowni, a twoje mięso i warzywa muszą być gotowe w mgnieniu oka.\r\nSosy na turbo: Twoje sosy to twoje tajne bronie. Wybierz te o największym przyspieszeniu! Ostry czosnek, pikantny harissa czy pikantny kebabowy sos – to one dadzą ci przewagę.\r\nLekkość: Unikaj zbędnych dodatków. Niech twoje nadzienie będzie lekkie jak piórko. Mniej to więcej!\r\nAerodynamika: Wybierz cienkie placki na kebaba. Im mniej oporu powietrza, tym szybciej będziesz się poruszać.\r\nMotywacja: Wyobraź sobie, że jesteś na torze wyścigowym. Widzisz linię mety, a tłumy ludzi dopingują cię do przodu. To właśnie teraz jest twój moment!\r\nI pamiętaj, że być może nie osiągniesz prędkości samochodu sportowego, ale z pewnością będziesz najszybszym kebabem w okolicy! ", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Komputery") || x.CategoryName.Equals("Sieci")).ToList();
                AttachmentSource = ("szop_ninjago.jpg");
                posts = new PostEntity(user,"Szop Ninjago", "Na długo zanim czas uzyskał imię, Pierwszy Mistrz Spinjitzu stworzył krainę Ninjago, używając do tego czterech magicznych Broni Spinjitzu: Kosy Wstrząsów,\r\n\r\n Nunczako Piorunów, Shurikenów Lodu, oraz Miecza Płomieni. Są tak potężne, że nikt nie może władać wszystkimi jednocześnie. \r\n\r\n Gdy Pierwszy Mistrz odszedł, jego dwaj synowie przysięgli, że będą strzec broni, jednak starszy z nich dał się zwieść ciemności i postanowił je ukraść. Doszło do ostatecznego pojedynku,\r\n\r\n młodszy z braci zwyciężył i strącił starszego do Krainy Cieni. Nastał pokój, a bronie zostały ukryte w \r\n\r\n bezpiecznym miejscu, młodszy brst wiedział jednak, że jego przeciwnik zaatakuje ponownie. Na straży broni postawił potężnego obrońcę, a prowadzącą do nich mapę powierzył człowiekowi o prawym sercu. (Koniec retrospekcji.) Tym wybrańcem był twój ojciec.\r\n\r\n A starszym bratem właśnie Lord Garmadon. Muszę odnaleźć 4 Bronie Spinjitzu, zanim jemu się to uda.", AttachmentSource, categories);
                _context.Add(posts);

                ReportPostEntity reportPost = new ReportPostEntity();
                reportPost.postId = 7;
                reportPost.ReportedPost = posts;
                reportPost.ReportContent = "co? szop zielonym ninją?!";
                _context.Add(reportPost);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Komputery")).ToList();
                AttachmentSource = ("szopdzilla.jpg");
                posts = new PostEntity(user1, "Ratuj się kto może", "Świeże drożdże ocieplić w temperaturze pokojowej. Przygotować rozczyn: drożdże rozpuścić w ciepłej wodzie, dodać 2 łyżki mąki oraz cukier, dokładnie wymieszać i odstawić na ok. 10 minut do wyrośnięcia (rozczyn ze świeżych drożdży zwiększa objętość o ok. 3 razy - jeśli tak się nie stanie proces przygotowania rozczynu trzeba powtórzyć od nowa, natomiast rozczyn z drożdży instant może się tylko trochę spienić).\r\nMąkę przesiać do miski, wymieszać z solą, zrobić wgłębienie w środku i wlać w nie rozczyn. Sukcesywnie zagarniać łyżką mąkę do środka i przez 2 - 3 minuty mieszać składniki, pod koniec dodając jeszcze oliwę.\r\nPołączone składniki wyłożyć na stolnicę oprószoną mąką. Wyrabiać przez ok. 15 minut aż ciasto będzie elastyczne i gładkie (ciasto można też zagnieść mikserem planetarnym).\r\nWyrobione ciasto włożyć do dużej miski, przykryć ściereczką i odstawić na ok. 1 godzinę do wyrośnięcia.\r\nWyrośnięte ciasto wyjąć na stolnicę i chwilę pozagniatać. Podzielić na 2 części, uformować z nich kulki i odłożyć na ok. 7 minut pod ściereczką.\r\nBlaszki (tortownice) posmarować oliwą. Włożyć na środek kulkę ciasta, delikatnie spłaszczyć i rozciągać, rozprowadzając palcami po całej powierzchni dna, zaczynając od środka i zostawiając niewielki \"wałeczek\" na brzegu (zob. zdjęcia poniżej). UWAGA: najlepiej robić to kilkoma etapami, ciasto na początku sprężynuje i \"cofa się\" ale jeśli odczekamy chwilę będziemy mogli je dalej rozciągać.\r\nWyłożyć cienką warstwę SOSU POMIDOROWEGO, ser* oraz ulubione dodatki. Odczekać ok. 15 minut aż ciasto podrośnie, następnie piec w maksymalnie nagrzanym piekarniku (min. 250 st. C) przez ok. 10 minut.", AttachmentSource, categories);
                _context.Add(posts);

                ReportPostEntity reportPost2 = new ReportPostEntity();
                reportPost2.postId = 8;
                reportPost2.ReportedPost = posts;
                reportPost2.ReportContent = "gdzie szop kong";
                _context.Add(reportPost2);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Komputery")).ToList();
                AttachmentSource = ("szop_potter.jpg");
                posts = new PostEntity(user2, "Tylko nie sliterin!!!", "szop który był wybrańcem", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Komputery") || x.CategoryName.Equals("Sieci")).ToList();
                AttachmentSource = ("szop_czarny_pan.jpg");
                posts = new PostEntity(user, "Insygnia śmieri", "Pewnego razu byli sobie trzej bracia, którzy wędrowali razem.\r\n Doszli do niebezpiecznej rzeki, której nie mogli przejść. Byli sprawnymi czarodziejami,\r\n więc wyczarowali nad wodą most. Gdy go przechodzili, drogę zagrodziła im Śmierć, która była wściekła, że ją oszukali. Udała, że ich podziwia\r\n i zaproponowała im nagrodę. Najstarszy brat o wojowniczym usposobieniu poprosił o najpotężniejszą różdżkę. Kostucha podeszła do najstarszego,\r\n rosnącego nad brzegiem rzeki drzewa, z którego gałęzi wykonała różdżkę. Drugi, nieco młodszy brat chciał jeszcze bardziej upokorzyć Śmierć,\r\n więc zażądał mocy wskrzeszenia umarłych. Śmierć podarowała mu niewielki kamień i zapewniła, że ma moc wzywania martwych zza grobu.\r\n Najmłodszy brat, który był z tej trójki także najskromniejszy i najmądrzejszy, nie ufał kostusze. Poprosił o coś, co pomoże mu odejść, nie będąc ściganym przez Śmierć.\r\n Ofiarowano mu Pelerynę Niewidkę. Następnie bracia przeszli przez most i powędrowali dalej, wkrótce rozchodząc się w różne strony.\r\n Najstarszy przez parę tygodni wędrował do wioski, a na miejscu odnalazł czarodzieja, z którym się kiedyś pokłócił. Ze swoją potężną różdżką (nazywaną teraz Czarną Różdżką) wyzwał go na pojedynek,\r\n który bez trudu wygrał. Po zamordowaniu przeciwnika udał się do gospody, gdzie przechwalał się jej mocą.\r\n Kiedy pijany spał tej nocy w owej karczmie, inny czarodziej podkradł się do jego łoża, zabrał mu różdżkę i poderżnął gardło. Drugi brat udał się do swojego domu, gdzie mieszkał samotnie.\r\n Wziął otrzymany kamień i obrócił go trzykrotnie w dłoni. Ujrzał dziewczynę, z którą planował się ożenić, dopóki nie zmarła przedwcześnie.\r\n Nie należała jednak do tego świata, i cierpiała oddalona od świata zmarłych. Widząc to, mężczyzna popełnił z rozpaczy samobójstwo, aby się z nią połączyć.\r\n Po zabraniu dwóch starszych braci, Śmierć przez wiele lat szukała najmłodszego z rodzeństwa. Ten, dopiero gdy dożył sędziwego wieku,\r\n zdjął z siebie Pelerynę i dał ją swojemu synowi. Wówczas pozdrowił Śmierć jak starego przyjaciela,\r\n po czym oboje odeszli z tego świata.", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Elektryka") || x.CategoryName.Equals("Komputery")).ToList();
                AttachmentSource = ("szop_batman.jpg");
                posts = new PostEntity(user1, "Something in the way", "zło nigdy nie śpi", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Elektronika") || x.CategoryName.Equals("Komputery") || x.CategoryName.Equals("Sieci")).ToList();
                AttachmentSource = ("szop_szambo.jpg");
                posts = new PostEntity(user2, "Szambo Nurek", "zagrożony wygnięciem. Szambo nuerk najmniejszy przerzuwać świata", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Komputery") || x.CategoryName.Equals("Sieci")).ToList();
                AttachmentSource = ("szop_pracownik.jpg");
                posts = new PostEntity(user, "My szopy", "nie mamy łatwo", AttachmentSource, categories);
                _context.Add(posts);

                categories = _context.Categories.Where(x => x.CategoryName.Equals("Komputery")).ToList();
                AttachmentSource = ("szop_skywalker.jpg");
                posts = new PostEntity(user1, "Nowa nadzije", "Nastały czasy wojny domowej. Statki Rebeliantów, atakujące z ukrytej bazy, odniosły pierwsze zwycięstwo w walce ze złowrogim Imperium Galaktycznym.\r\n\r\nSzpiedzy wykradli tajne plany ostatecznej broni Imperium, GWIAZDY SMIERCI, stacji kosmicznej o sile rażenia zdolnej zniszczyć całą planetę.\r\n\r\nŚcigana przez agentów Imperium, Księżniczka Leia ucieka do domu, strzegąc wykradzionych planów, które mogą ocalić jej lud i przywrócić wolność galaktyce.", AttachmentSource, categories);
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
                var chatEntity1 = new ChatEntity();
                var user11 = _context.Users.FirstOrDefault(u => u.Email == "test@user");
                var user12 = _context.Users.FirstOrDefault(u => u.Email == "test2@user");
                chatEntity1.ChattingUser1 = user11;
                chatEntity1.User1Nick = user11.Nick;
                chatEntity1.ChattingUser2 = user12;
                chatEntity1.User2Nick = user12.Nick;
                _context.Add(chatEntity1);

                MessageEntity messageEntity1 = new MessageEntity();
                messageEntity1.MessageContent = "Rozpoczęcie chatu";
                messageEntity1.UsedChat = chatEntity1;
                messageEntity1.currentChat = chatEntity1.Id;
                messageEntity1.UsingUser = user12;
                messageEntity1.SendDate = DateTime.Now;
                _context.Add(messageEntity1);

                var chatEntity2 = new ChatEntity();
                var user21 = _context.Users.FirstOrDefault(u => u.Email == "test@user");
                var user22 = _context.Users.FirstOrDefault(u => u.Email == "test3@user");
                chatEntity2.ChattingUser1 = user21;
                chatEntity2.User1Nick = user21.Nick;
                chatEntity2.ChattingUser2 = user22;
                chatEntity2.User2Nick = user22.Nick;
                _context.Add(chatEntity2);

                MessageEntity messageEntity2 = new MessageEntity();
                messageEntity2.MessageContent = "Rozpoczęcie chatu";
                messageEntity2.UsedChat = chatEntity2;
                messageEntity1.currentChat = chatEntity2.Id;
                messageEntity2.UsingUser = user21;
                messageEntity2.SendDate = DateTime.Now;
                _context.Add(messageEntity2);

                _context.SaveChanges();
            }
        }
    }
}
