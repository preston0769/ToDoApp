using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;

namespace ToDo
{
    public class ToDoContext:DbContext
    {
        public ToDoContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer( @"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;" );
            base.OnConfiguring(optionsBuilder);

        }


        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
