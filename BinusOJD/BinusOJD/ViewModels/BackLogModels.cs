using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BinusOJD.Models;

namespace BinusOJD.ViewModels
{
    public class BackLogModels
    {
        public Sprint Sprint { get; set; }
        public WorkItem WorkItem { get; set; }
        public Task Task { get; set; }
    }
}