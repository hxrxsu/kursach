using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string FLP { get; set; } //First Name Last Name Patronymic
        public string Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } 
    }
}
