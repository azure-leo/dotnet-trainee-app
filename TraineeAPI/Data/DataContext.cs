global using Microsoft.EntityFrameworkCore;

namespace TraineeAPI.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Article> Articles { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);
    //     optionsBuilder.UseSqlServer("");
    // }
}