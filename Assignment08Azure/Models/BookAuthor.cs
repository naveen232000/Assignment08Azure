using System;
using System.Collections.Generic;

namespace Assignment08Azure.Models
{
    public partial class BookAuthor
    {
        public int BookAuthorId { get; set; }
        public int? BookId { get; set; }
        public int? Author { get; set; }

        public virtual Author? AuthorNavigation { get; set; }
        public virtual Book? Book { get; set; }
    }
}
