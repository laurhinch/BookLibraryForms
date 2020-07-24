using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryForm
{
    public class Book
    {
        public Book(string Title, string Author, string Location, string Status)
        {
            title = Title;
            author = Author;
            location = Location;
            status = Status;
        }

        public string title;
        public string author;
        public string location;
        public string status;

        public string toString()
        {
            return title + " | " + author + " | " + location + " | " + status;
        }
    }
}
