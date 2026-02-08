using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entidades
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
