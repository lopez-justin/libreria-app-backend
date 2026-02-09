using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        [JsonIgnore]
        public virtual AuthUser? AuthUser { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public string? Location { get; set; } = null!;
        public string? Avatar { get; set; }
        public DateTime MemberSince { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; } = new List<Book>();
        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        [JsonIgnore]
        public ICollection<Transaction> RequestedTransactions { get; set; } = new List<Transaction>();
        [JsonIgnore]
        public ICollection<Transaction> OwnedTransactions { get; set; } = new List<Transaction>();
    }

}
