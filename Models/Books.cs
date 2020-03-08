using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AndersonBookShop.Models
{
    public class Books
    {
        public int BookID { get; set; }
        [DisplayName("Book")]
        public string BookName { get; set; }
        public string Author { get; set; }
        public int Published { get; set; }
        public string Available { get; set; }
    }
}