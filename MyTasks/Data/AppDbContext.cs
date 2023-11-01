using Microsoft.EntityFrameworkCore;
using MyTasks.Models;


namespace MyTasks.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<ToDo> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<ToDo>(e =>
            {
                e.HasKey(t => t.Id);
                e.Property(t => t.Title);
                e.Property(t => t.Date);

            });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");

    }
}
