using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Models.Context;

public class ApplicationDBContext : DbContext
{
    
    public ApplicationDBContext() {}
    
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
    
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost,1433; Database=LibreriaDB; User Id=sa; Password=Justin-23022004; TrustServerCertificate=True");

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
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

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
        
        modelBuilder.Entity<AuthUser>()
            .HasOne(a => a.User)
            .WithOne(u => u.AuthUser)
            .HasForeignKey<AuthUser>(a => a.UserId);

        modelBuilder.Entity<AuthUser>()
            .HasIndex(a => a.Email)
            .IsUnique();


        // ISBN único
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Isbn)
            .IsUnique();
    }

}