using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entidades
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Book>? Books { get; set; }
    }
}
