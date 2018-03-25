using System.Collections.Generic;
namespace ProjectEng.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        //Relations
        public ICollection<Project> Projects { get; set; }
    }
}
