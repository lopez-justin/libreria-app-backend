using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Models.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        [JsonIgnore]
        public virtual Book? Book { get; set; } = null!;

        public int RequesterId { get; set; }
        [JsonIgnore]
        public virtual User? Requester { get; set; } = null!;

        public int OwnerId { get; set; }
        [JsonIgnore]
        public virtual User? Owner { get; set; } = null!;

        public String Type { get; set; } = null!;

        public decimal? Price { get; set; }

        public String Status { get; set; } = null!;

        public DateTime RequestDate { get; set; }

        public DateTime? ResponseDate { get; set; }

        public string? Message { get; set; }
    }

}
