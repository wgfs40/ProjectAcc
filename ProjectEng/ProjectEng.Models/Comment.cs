namespace ProjectEng.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public string Comments { get; set; }
        public short Stage { get; set; }
        public string Username { get; set; }

        //Relations
        public virtual Task Taks { get; set; }
    }
}
