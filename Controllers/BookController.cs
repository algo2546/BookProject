using BooksSpring2024_sec02.Data;
using BooksSpring2024_sec02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksSpring2024_sec02.Controllers
{
    public class BookController : Controller
    {
        private BooksDBContext _dbContext;

        public BookController(BooksDBContext context)
        {
            _dbContext = context; // passes the dbContext object to the instance variable. this is how you inject your database into the controller, called dependency injection

        }

        public IActionResult Index()

        {
            var listOfBooks = _dbContext.Books.ToList();

            return View(listOfBooks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> listOfCategories = _dbContext.Categories.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });

            // PROJECTION: allows us to project a category object to a SelectListItemobject, where the name of the category is used as the Text and the CategoryId is used as the Value of the SelectListItem    

            //ViewBag
            ViewBag.ListOfCategories = listOfCategories;

            return View();

        }

    

        [HttpPost]
        public IActionResult Create(Book bookObj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Books.Add(bookObj);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Book");
            }

            return View(bookObj);
        }

    }
}
