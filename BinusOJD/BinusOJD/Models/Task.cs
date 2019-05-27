using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BinusOJD.Models
{
    public class Task
    {
        public int IDTask { get; set; }
        public int IDWorkItem { get; set; }
        public int AssignTo { get; set; }
        public string UsernameAssignTo { get; set; }
        public string Title { get; set; }        
        public int IDState { get; set; }
        public string StateName { get; set; }
    }
}