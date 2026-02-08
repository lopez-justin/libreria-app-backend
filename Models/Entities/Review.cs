using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int Rating { get; set; }

        public string Comment { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public bool Active { get; set; }
    }

}
