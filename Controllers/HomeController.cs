using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotels.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;              No Need
        //                   ILogger<HomeController> logger
        // public HomeController()
        //{
        //_logger = logger;
        //}

        

        private readonly ApplicationDbContext _context; // we add this to insert data

        public HomeController(ApplicationDbContext context)
        {
            _context = context;// add this
        }
        public IActionResult CreateNewRecord(Hotel hotels)// add this
        {
            if(ModelState.IsValid) // for security test them with model itself 
            {
				_context.hotel.Add(hotels);// add this
				_context.SaveChanges();// add this
				return RedirectToAction("Index");// add this
			}

			//return View();
            var hotel = _context.hotel.ToList();
			return View("Index", hotel);
		}

        public IActionResult Delete(int id)
        {
            var hoteldelete = _context.hotel.SingleOrDefault(x => x.Id == id); // search
            _context.hotel.Remove(hoteldelete); // Delete
            _context.SaveChanges(); // Save
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id) // part 1 update
        {
            var hoteledit = _context.hotel.SingleOrDefault(x => x.Id == id); // search
            
            return View(hoteledit);
        }

        public IActionResult Update( Hotel hotel) // part 2 update
        {
            if (ModelState.IsValid) { 
            _context.hotel.Update(hotel); // Update
            _context.SaveChanges();
            return RedirectToAction("Index");
            }

            return View("Edit");
        }

        public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();
            // u can use view bag
            return View(hotel); // Another way to view data imediatily when data inserted 
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}