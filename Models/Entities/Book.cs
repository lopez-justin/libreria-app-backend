namespace Models.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Isbn { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public String Condition { get; set; } = null!;

        public decimal? Price { get; set; }

        public String Type { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string Location { get; set; } = null!;
        
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

}
