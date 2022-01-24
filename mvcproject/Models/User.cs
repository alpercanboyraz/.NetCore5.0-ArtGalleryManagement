using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvcproject.Models
{
    public class User
    {
        public int id { get; set; }

       
        public string name { get; set; }
        public string surname { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public string city { get; set; }

        public string country { get; set; }

        public string street { get; set; }

        
        public string password { get; set; }

        public List<User> userinfo { get; set; }
    }
}
