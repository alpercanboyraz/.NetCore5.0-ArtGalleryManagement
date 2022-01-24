using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcproject.Models
{
    public class ArtWork
    {
        public int art_id { get; set; }
        public string title { get; set; }
        public int category_id { get; set; }
        public decimal price { get; set; }
        public string file_location { get; set; }
        public int user_id { get; set; }
        public string product_explanation { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<User> Users{ get; set; }

    }
}
