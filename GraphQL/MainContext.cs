using GraphQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions opt): base(opt) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId);

            modelBuilder.Entity<Author>()
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}