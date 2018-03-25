using ProjectEng.Models;
using ProjectEng.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEng.Web.ViewModels
{
    public class VMDashboard
    {
        public IList<Dashboard> Dashboard { get; set; }
        public IList<Task> Task { get; set; }
    }
}