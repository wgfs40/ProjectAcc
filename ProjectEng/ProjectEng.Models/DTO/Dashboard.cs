using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Models.DTO
{
    public class Dashboard
    {
        public string CustomerName { get; set; }
        public int ProjectID { get; set; }
        public string CMStaff { get; set; }
        public string PMStaff { get; set; }
        public string PDEStaff { get; set; }
        public string SEStaff { get; set; }
        public string QEStaff { get; set; }
        public string DRStaff { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime LaunchDate { get; set; }

    }
}
