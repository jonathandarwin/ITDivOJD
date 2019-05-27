using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BinusOJD.Models
{
    public static class SESSION
    {
        public static int IDUser { get; set; }
        public static string Username { get; set; }
        public static int Role { get; set; }
        public static string PhotoPath { get; set; }
    }
}