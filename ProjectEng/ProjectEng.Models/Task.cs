using System;
using System.Collections.Generic;
namespace ProjectEng.Models
{
    public class Task
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int? CurrentState { get; set; }
        public DateTime Create { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Responsable { get; set; }

        //Relations
        public virtual Project Projects { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
