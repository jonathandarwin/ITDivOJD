using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BinusOJD.Models
{
    public class WorkItem
    {        
        public int IDWorkItem { get; set; }
        public int IDSprint { get; set; }
        public string Title { get; set; }
        public int IDState { get; set; }
        public string StateName { get; set; }
        public List<Task> Task { get; set; }
    }
}