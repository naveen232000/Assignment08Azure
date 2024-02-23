using System;
using System.Collections.Generic;

namespace Assignment08Azure.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = null!;
        public string? PublisherAddress { get; set; }
        public string? MobileNumber { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
