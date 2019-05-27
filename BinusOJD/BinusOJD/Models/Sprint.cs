using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BinusOJD.Models
{
    public class Sprint
    {
        public int IDSprint { get; set; }
        public int IDProject { get; set; }
        public string SprintName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Section { get; set; }
    }
}