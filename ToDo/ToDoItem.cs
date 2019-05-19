using System;

namespace ToDo
{
    public class ToDoItem
    {
        public ToDoItem(string description)
        {
            Id = Guid.NewGuid(); 
            Description = description;

        }
        public Guid Id{ get; set; }
        public string Description { get; set; }

        public Status status { get; set; }
    }

    public enum Status
    {
        New,
        Completed
    }
}

