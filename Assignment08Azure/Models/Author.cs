using System;
using System.Collections.Generic;

namespace Assignment08Azure.Models
{
    public partial class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
            Books = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
