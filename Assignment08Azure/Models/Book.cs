using System;
using System.Collections.Generic;

namespace Assignment08Azure.Models
{
    public partial class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public int BookId { get; set; }
        public int? Author { get; set; }
        public int? Publisher { get; set; }
        public int? Category { get; set; }
        public double Price { get; set; }
        public string BookName { get; set; }

        public virtual Author? AuthorNavigation { get; set; }
        public virtual BookCategory? CategoryNavigation { get; set; }
        public virtual Publisher? PublisherNavigation { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
