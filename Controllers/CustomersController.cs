using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;


namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context; // dostęp do DbContext z klasy IdentityModels
                                                //dla każdej klasy (np membershipType itp)

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Tworzymy formularz dla klienta oraz widok New
        public ActionResult New()
        {
            //pobieramy listę typów czonkostwa z bazy danych
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        //zastosowanie model binding czyli MVC automatycznie mapuje dane żądania do tego obiektu (customer)
        [HttpPost]
        [ValidateAntiForgeryToken] //aby zakodować przed hakerami formularz nalerzy użyć takiej data annotations oraz w widoku dodać @Html.AntiForgeryToken()
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer, // jest to wymagane do wypłenienia formularza danymi przez użytkownika
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
             _context.Customers.Add(customer); //dodajemy dbcontext aby połączyć się z bazą danych w celu zapisu customers
            }
            else
            {
                //tworzymy dbContext do sledzenia zmian w obiekcie / oraz pobieramy pojedynczego klienta po jego Id
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                //ręczna aktualizacja wartości w bazie danych dla każdego formularza z lekcji aktualizajca klienta (updating data)

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges(); // do zapisu w bazie danych
            //przekierowuje do określonej akcji, używając nazwy i słownika czyli kontrolera (np. Customers)
            return RedirectToAction("Index", "Customers");
        }

        // GET: Customer
        public ViewResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            //pobieramy customer id z bazy danych
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            //jeżeli dany klient istnieje w bazie danych zostanie zwrócony, jezeli nie to otrzymamy null i musimy to sprawdzić
            if (customer == null)
            {
                return HttpNotFound(); // zwraca standardowy błąd 404
            }

            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList() //inicjalizacja ta pozwala na pobranie membershipType z bazy prosto tutaj
            };
            //zwracamy klienta
            return View("CustomerForm", viewModel);//bez tego MVC będzie szukało akcji Edit a nie CustomerForm w której znajdują się pola do wypełnienia danych przez użytkownika
        }
    }
}
/*  ModelState
 Reprezentuje zbiór par nazw i wartości, które zostały przesłane do serwera podczas testu POST.
 Zawiera także zbiór komunikatów o błędach dla każdej przesłanej wartości. 
 Mimo swojej nazwy, tak naprawdę nic nie wie o żadnych klasach modeli, ma tylko nazwy, wartości i błędy.
*/