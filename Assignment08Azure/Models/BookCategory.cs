using System;
using System.Collections.Generic;

namespace Assignment08Azure.Models
{
    public partial class BookCategory
    {
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }

        public int CategoryId { get; set; }
        public string Category { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
