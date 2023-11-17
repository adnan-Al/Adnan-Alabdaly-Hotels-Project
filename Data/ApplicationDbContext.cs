
using Hotels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Hotels.Data
{
    public class ApplicationDbContext:DbContext // it is important to add this and import its library 
    {

        // write ctor then click Tab to build the constructor -- Alt + Enter shows u solutions and import classes 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) // it is important to add this class as a paramter 
        {
            
        }
        // models 
        public DbSet<Cart> carts {  get; set; }
        public DbSet<Hotel> hotel { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<RoomDetails> roomDetails { get; set; }
        public DbSet<Rooms> rooms { get; set; }

    }
}
