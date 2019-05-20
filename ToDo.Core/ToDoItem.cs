using System;

namespace ToDo
{
    public class ToDoItem
    {
        public ToDoItem( Guid id, string description)
        {
            Id = id;
            Description = description;

        }
        public ToDoItem(string description)
        {
            Id = Guid.NewGuid(); 
            Description = description;

        }
        public Guid Id{ get; set; }
        public string Description { get; set; }

        public Status status { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Description} - {status}";
        }
    }

    public enum Status
    {
        New,
        Completed
    }
}

