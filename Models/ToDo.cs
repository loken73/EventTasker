using System;

namespace EventTasker.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }
        
        public bool IsComplete { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

}