using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Tasks
    {
        public long TaskID { get; set; }
        public string Employee { get; set; }
        public string Task { get; set; }
        public string Details { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public DateTime? Deadline { get; set; }
        public string Status { get; set; }
    }
}