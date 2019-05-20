using System;

namespace ToDo
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = ConsoleApp.Instance;


            while (true)
            {

                var command = app.ReadCommand();

                if (command != null)
                    app.ExecuteComand(command);
                else
                    Console.WriteLine("Sorry I can not understand your command");
            }

        }

    }


}

