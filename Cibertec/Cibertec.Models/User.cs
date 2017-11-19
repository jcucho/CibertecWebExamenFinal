using System;
using System.Collections.Generic;
using System.Text;

namespace Cibertec.Models
{
    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
