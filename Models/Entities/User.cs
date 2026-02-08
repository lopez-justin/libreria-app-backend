using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Location { get; set; } = null!;

        public string? Avatar { get; set; }

        public decimal Rating { get; set; }

        public int TotalBooks { get; set; }

        public DateTime MemberSince { get; set; }

        public bool Active { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Transaction> RequestedTransactions { get; set; } = new List<Transaction>();
        public ICollection<Transaction> OwnedTransactions { get; set; } = new List<Transaction>();
    }

}
