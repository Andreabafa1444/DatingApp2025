using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //constructor de la clase
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}