using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ToDo
{
    public class ToDoManager
    {
        private readonly ILogger<ToDoManager> _loggger;
        private readonly ToDoContext _dbContext;

        public ToDoManager(ILogger<ToDoManager> logger,
            ToDoContext toDoContext
            )
        {
            _loggger = logger;
            _dbContext = toDoContext;
            ToDoList = new List<ToDoItem>();
        }

        public List<ToDoItem> ToDoList
        {
            get
            {
                return _dbContext.ToDoItems.ToList();
            }
            set
            {
            }
        }

        public void AddToDoItem(ToDoItem item)
        {
            _loggger.LogInformation("An item has been added to repo");

            _dbContext.ToDoItems.Add(item);

            _dbContext.SaveChanges();
           
        }

        public void UpdateToDoItem(ToDoItem newItem)
        {
            var oldItem = ToDoList.Where(x => x.Id == newItem.Id).FirstOrDefault();

            var entity = _dbContext.ToDoItems.Where(x => x.Id == newItem.Id).FirstOrDefault();

            if (entity == null)
                throw new Exception("Entity Not found");
            entity = newItem;

            _dbContext.Entry(entity).State = EntityState.Modified; 

            _dbContext.SaveChanges();

        }

        public void CompleteTodoItem(Guid id)
        {
            var entitiy = _dbContext.ToDoItems.Where(x => x.Id == id).FirstOrDefault();

            if(entitiy == null)
                throw new Exception("Entity Not found");

            entitiy.status = Status.Completed;

            _dbContext.SaveChanges();
        }

    }
}

