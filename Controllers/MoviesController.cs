using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidly.Models;
using Vidly.ViewModel;
using System.Data.Entity;
using System.Web.UI.WebControls;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            //var movie = _context.Movies.Include(m => m.Genre).ToList();
            //właściwośc użytkownika naszego kontrolera, któa daje nam dostęp do obecnego użytkownika 
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            else
                return View("ReadOnlyList");
        }

        //Tworzymy formularz dla Genre oraz widok MovieForm
        [Authorize(Roles = RoleName.CanManageMovies)] // tutaj ustawiamy właściwości roli aby zwykły użytkownik całkowicie nie mógł ingerować w edycjęfilmu
        public ViewResult New()
        {
            var genre = _context.Genres.ToList(); // pobieramy liste filmów z tabeli Genres

            var viewModel = new MovieFormViewModel()
            {
                //Movie = new Movie(),
                Genres = genre
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    //Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie); //dodajemy dbcontext aby połączyć się z bazą danych w celu zapisu Movie
            }
            else
            {
                //tworzymy dbContext do sledzenia zmian w obiekcie / oraz pobieramy pojedynczego klienta po jego Id
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                //ręczna aktualizacja wartości w bazie danych dla każdego formularza z lekcji aktualizajca klienta (updating data)
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges(); // zapis do bazy danych

            //przekierowuje do określonej akcji, używając nazwy i słownika czyli kontrolera (np. Movie)
            return RedirectToAction("Index", "Movies");
        }
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        //Dodanie formularza edytowalnego
        public ActionResult Edit(int id)
        {
            //pobieramy movie id z bazy danych
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel(movie) //po dodaniu movie musisz dodać konstruktor w MovieFormViewModel i przekazac w nim Movie movie
            {
                Movie = movie,
                Genres = _context.Genres.ToList() //inicjalizacja ta pozwala na pobranie Genres z bazy prosto tutaj
            };

            return View("MovieForm", viewModel);
        }

        /*
        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shrek!"};
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }
        */
    }
}

/*
// GET: Movies
public ActionResult Random() // Index() - zwraca ActionResult i zmieinliśmy to na Random()
{
    var movie = new Movie() {Name = "Shrek!"};//w prawidziwej aplikacji często dostajemy model z bazy danych
                                                //ale narazie zrobimy to manualnie

    //Lista klientów oraz inicjalizacja obiektu randomMovieViewModel jest naszym modelem Views
    var customers = new List<Customer>()
    {
        new Customer {Name = "Customer 1"},
        new Customer {Name = "Customer 2"}
    };

    var viewModel = new RandomMovieViewModel()
    {
        Movie = movie,
        Customers = customers
    };


    return View(viewModel);

    //return View(movie);//ten View narazie świeci się na czerwono do póki nie dodamy klasy w folderze Movies
    //return Content("Hello World");
    //return HttpNotFound();
    //return new EmptyResult();
    //return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"}); // wpierw podajemy nazwę akcji a potem kontroler, trzeci argument to obiekt anonimowy
    */
    //}
//}

/*
//poniżej znajduje się atrybut Route i tutaj w regex (pozwala na użycie regularnego wyrażęnia) musimy podać dwa ukośniki !
// regex i range to constrain reszta opisana w zeszycie
[Route("movies/released/{year}/ {month:regex(\\d{4}):range(1,12)}")]
public ActionResult ByReleaseDate(int year, int month)
{
    return Content(year + "/" + month);
}
*/

/*
public ActionResult Edit(int id)//prezentacja mapowania id oraz tego jak wygląda wyjątek Sever Error in '/' Application
{
    return Content("id=" + id);
}

public ActionResult Index(int? pageIndex, string sortBy) // ta akcja zostanie wywołana gdy przejdziemy do filmów i zwrócimy liste filmów z bazy danych
{                           //pageIndex wyswietli filmy na str 1 i upewniamy się że nie jest null, sortBy sortuje po name
    if (!pageIndex.HasValue) // jeżeli nie ma wartości ustaw pageIndex na 1
    {
        pageIndex = 1;
    }

    if (string.IsNullOrWhiteSpace(sortBy)) // jeżeli string jest null albo zaiwera białe znaki ustaw sortBy na Name
    {
        sortBy = "Name";
    }

    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
}
*/
//}
//}
/*       Action Results
 *  Type                Helped Method
 *  ViewResult          View()
 *  PartialViewResult   PartialView()
 *  ContentResult       Content()
 *  RedirectResult      Redirect()
 *  RedirectToRouteResult   RedirectToAction()
 *  JsonResult          Json()
 *  FileResult          File()
 *  HttpNotFoundResult  HttpNotFound()
 *  EmptyResult         -------------
 * Najważniejsze to ViewResult, RedirectResult oraz HttopNotFoundResult
*/

/* sortBy (to jest z JS):
 * To samo co toArray (), ale z ręcznym sortowaniem zastosowanym do tablicy.
 * Podobne do Table.orderBy (), ale sortuje wynikową tablicę, zamiast pozwolić sortowaniu na implementację backendu.
*/

/*Format()
 W języku c # metoda Format ciągu służy do wstawiania wartości zmiennej lub obiektu lub wyrażenia do innego ciągu. 
 Korzystając z metody Format ciągu, możemy zastąpić elementy formatu w określonym ciągu ciągiem reprezentującym określone obiekty.
*/
