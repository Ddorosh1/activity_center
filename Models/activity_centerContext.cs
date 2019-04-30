using Microsoft.EntityFrameworkCore;
 
namespace activity_center.Models
{
    public class activity_centerContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public activity_centerContext(DbContextOptions options) : base(options) { }

         public DbSet<User> Users { get;set; }
         public DbSet<Activity> Activity { get; set; }
         public DbSet<Association> Associations { get;set; }
    }
}