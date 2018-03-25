
namespace ProjectEng.Models
{
    public class ProjectStaff
    {
        public int ProjectId { get; set; }
        public string CMStaff { get; set; }
        public string PMStaff { get; set; }
        public string PDEStaff { get; set; }
        public string SEStaff { get; set; }
        public string QEStaff { get; set; }
        public string DRStaff { get; set; }

        //Relations
        public virtual Project Projects { get; set; }
    }
}
