using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Models.Context;

public class ApplicationDBContext : DbContext
{
    
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
    
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category -> Books (1:N)
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId);

        // User -> Books (1:N)
        modelBuilder.Entity<Book>()
            .HasOne(b => b.User)
            .WithMany(u => u.Books)
            .HasForeignKey(b => b.UserId);

        // Book -> Reviews (1:N)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);

        // User -> Reviews (1:N)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);

        // Transaction -> Book (1:N)
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Book)
            .WithMany(b => b.Transactions)
            .HasForeignKey(t => t.BookId);

        // Transaction -> User (Requester)
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Requester)
            .WithMany(u => u.RequestedTransactions)
            .HasForeignKey(t => t.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);

        // Transaction -> User (Owner)
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Owner)
            .WithMany(u => u.OwnedTransactions)
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        // ISBN único
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Isbn)
            .IsUnique();
    }

}