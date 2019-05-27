using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BinusOJD.Models
{
    public class User
    {
        public int IDUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string PhotoPath { get; set; }
        public bool Status { get; set; }
        public bool isAuth { get; set; }
    }
}