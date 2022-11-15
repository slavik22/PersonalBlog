using Data.Entities;
using Microsoft.EntityFrameworkCore;

//using Microsoft.Extensions.Configuration;

namespace Data.Data;
public class PersonalBlogDbContext : DbContext
{
    public PersonalBlogDbContext()
    {
    }

    public PersonalBlogDbContext(DbContextOptions<PersonalBlogDbContext> options) : base(options){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        optionsBuilder.UseSqlServer("Server=DESKTOP-OC1V65E;Database=PersonalBlogDB;Trusted_Connection=True;TrustServerCertificate=True");
    }

    public DbSet<Post>? Posts { get; set; }
    public DbSet<Comment>? Comments { get; set; }
    public DbSet<User>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Author)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
    

}