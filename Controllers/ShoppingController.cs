using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Controllers
{
    public class ShoppingController : Controller
    {
        private ApplicationDbContext _context;

        public ShoppingController(ApplicationDbContext context)
        {
            _context = context;//to connect to db
        }
        public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();// to read the data
            return View(hotel);
            
        }

        public IActionResult Rooms(int id) { 
        var rooms = _context.rooms.Where(p=>p.IdHotel== id).ToList();
            return View(rooms);

        }

        //public IActionResult Invoice(int id)
        //{
        //    var rooms = _context.rooms.SingleOrDefault(p => p.Id ==id);

        //    var Invoice = new Invoice()
        //    {
        //        IdRooms = rooms.Id,
        //        IdHotel = rooms.IdHotel,
        //        IdRoomDetails = rooms.Id,
        //        Price = rooms.Price,
        //        Total = rooms.Price * 1,


        //    };
        //    _context.invoices.Add(Invoice);
        //    _context.SaveChanges();

        //    return View(Invoice);            
        //}
    }
}
