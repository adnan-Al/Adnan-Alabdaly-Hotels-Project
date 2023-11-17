using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit; // google i added
using MailKit.Net.Smtp; // google i added
namespace Hotels.Controllers
{
	public class DashboardController : Controller
	{
		private readonly ApplicationDbContext _context;
		public DashboardController(ApplicationDbContext context)
		{
			_context = context;
		}
        //google email adbhfx@gmail.com
        // yqbosdikzxcxnlsm

        public async Task<string> SendEmail()
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Text message", "adbhfx@gmail.com"));
			message.To.Add(MailboxAddress.Parse("adnn225@hotmail.com"));
			message.Subject="Test Email from asp.net core mvc";
			message.Body = new TextPart("Plain")
			{
				Text = "Welcome in my app"
			};

			using(var client=new SmtpClient())
			{
				try
				{
					client.Connect("smtp.gmail.com", 587);
					client.Authenticate("adbhfx@gmail.com", "yqbosdikzxcxnlsm");
					await client.SendAsync(message);
					client.Disconnect(true);
				}
				catch { }
			}

			return "Ok";
		}

        [Authorize] // we add this to show login page and apply the scaffolding
		public IActionResult Index()
		{	
			var currentuser=HttpContext.User.Identity.Name;
			ViewBag.currentuser = currentuser;
            HttpContext.Session.SetString("UserName", currentuser);
            //TempData["currentuser"] = currentuser;
            //CookieOptions option = new CookieOptions();
            //options.Expires = DateTime.Now.AddMinutes(20);
            //Response.Cookies.Append("UserName", currentuser, option); // in order to save value and show them in multiple fields - cookies

            var hotel = _context.hotel.ToList();// to read the data

			return View(hotel);
		}

		[HttpPost]
		public IActionResult Index(string city)
		{
			var hotel = _context.hotel.Where(x => x.City.Equals(city));
			//var hotel = _context.hotel.ToList();// to read the data

			return View(hotel);
		}

		

		public IActionResult RoomDetails()
		{
            var currentuser = HttpContext.User.Identity.Name;
            ViewBag.currentuser = currentuser;
            HttpContext.Session.SetString("UserName", currentuser);


            var hotel = _context.hotel.ToList();
			ViewBag.Hotel = hotel;
			//ViewBag.currentuser = HttpContext.Session.GetString("UserName");
			var roomsdetails = _context.roomDetails.ToList();

			return View(roomsdetails);
		}

		public IActionResult CreateNewRoomDetails(RoomDetails roomDetails)
		{
			if(ModelState.IsValid) { 
			_context.roomDetails.Add(roomDetails);
			_context.SaveChanges();
			return RedirectToAction("RoomDetails");
            }
            var hotel = _context.hotel.ToList();
            return View("RoomDetails", hotel);
        }

		public IActionResult DeleteRoomDetails(int id)
		{
			var D_roomDetails = _context.roomDetails.SingleOrDefault(x => x.Id == id);
			if (D_roomDetails != null)
			{
				_context.roomDetails.Remove(D_roomDetails);
				_context.SaveChanges();
				TempData["RoomUpdated"] = "Deleted";

			}
			// add this
			return RedirectToAction("RoomDetails");
		}

		public IActionResult EditRoomDetails(int id) // part 1 update
		{
            var hotel = _context.hotel.ToList();
            ViewBag.Hotel = hotel;
            var EditroomDetails = _context.roomDetails.SingleOrDefault(x => x.Id == id); // search
            return View(EditroomDetails);
		}


		public IActionResult UpdateRoomDetails(RoomDetails roomDetails) // part 2 update
		{
			var hotel = _context.hotel.ToList();
			ViewBag.Hotel = hotel;
			if (ModelState.IsValid)
			{
				_context.roomDetails.Update(roomDetails); // Update
				_context.SaveChanges();
				return RedirectToAction("RoomDetails");
			}

			return View("EditRoomDetails");
		}


		public IActionResult Rooms()
		{
			var hotel = _context.hotel.ToList();
			ViewBag.Hotel = hotel;
			//ViewBag.currentuser = Request.Cookies["UserName"];
			ViewBag.currentuser = HttpContext.Session.GetString("UserName");
			var rooms = _context.rooms.ToList();

			return View(rooms);
		}

        public IActionResult EditRoom(int id) // part 1 update
        {
            var hotel = _context.hotel.ToList();
            ViewBag.Hotel = hotel;
            var roomedit = _context.rooms.SingleOrDefault(x => x.Id == id); // search

            return View(roomedit);
        }

        public IActionResult UpdateRoom(Rooms room) // part 2 update
        {
			var hotel = _context.hotel.ToList();
			ViewBag.Hotel = hotel;
			if (ModelState.IsValid)
            {
                _context.rooms.Update(room); // Update
                _context.SaveChanges();
                TempData["RoomUpdated"] = "Ok";
                return RedirectToAction("Rooms");
            }

            return View("EditRoom");
        }

        public IActionResult CreateNewRooms(Rooms rooms)
		{
			if (ModelState.IsValid) { 
			_context.rooms.Add(rooms);
			_context.SaveChanges();
			return RedirectToAction("Rooms");
            }
            var hotel = _context.hotel.ToList();
            return View("Rooms", hotel);
        }

        public IActionResult DeleteRoom(int Id)
        {
            var roomDel = _context.rooms.SingleOrDefault(x => x.Id == Id);
            if (roomDel != null)
            {
                _context.rooms.Remove(roomDel);
                _context.SaveChanges();
                TempData["RoomUpdated"] = "Deleted";

            }
            // add this
            return RedirectToAction("Rooms");// add this
        }

        public IActionResult CreateNewHotel(Hotel hotels)
		{
			if (ModelState.IsValid)
			{
				_context.hotel.Add(hotels);// add this
				_context.SaveChanges();// add this
				return RedirectToAction("Index");// add this
			}

			var hotel = _context.hotel.ToList();
			return View("Index", hotel);
		}

		public IActionResult Delete(int id)
		{
			var hotelDel = _context.hotel.SingleOrDefault(x => x.Id == id);
			if (hotelDel != null)
			{
				_context.hotel.Remove(hotelDel);
				_context.SaveChanges();
				TempData["Del"] = "Ok";

			}
			// add this
			return RedirectToAction("Index");// add this
		}

        public IActionResult Edit(int id) // part 1 update
        {
            var hoteledit = _context.hotel.SingleOrDefault(x => x.Id == id); // search

            return View(hoteledit);
        }

        public IActionResult Update(Hotel hotel) // part 2 update
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Update(hotel); // Update
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        

        //public IActionResult EditRoom(int Id)
        //{
        //	var roomDel = _context.rooms.SingleOrDefault(x => x.Id == Id);
        //	if (roomDel != null)
        //	{
        //		_context.rooms.Update(roomDel);
        //		_context.SaveChanges();
        //		TempData["Del"] = "Ok";

        //	}
        //	// add this
        //	return RedirectToAction("Index");// add this
        //}
    }
}
