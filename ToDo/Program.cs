using System;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace ToDo
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureSerilog();

            serviceCollection.AddLogging(configure=>configure.AddSerilog());
            serviceCollection.AddScoped<ToDoContext>();

            serviceCollection.AddScoped<ToDoManager>();


            var serviceProvider = serviceCollection.BuildServiceProvider();


            var todoManager = serviceProvider.GetService<ToDoManager>();

            todoManager.ToDoList.ForEach(x => Console.WriteLine($"{x.Id.ToString()}:{x.Description}"));

            todoManager.AddToDoItem(new ToDoItem("ToDo Item 2"));

            todoManager.ToDoList.ForEach(x => Console.WriteLine($"{x.Id.ToString()}:{x.Description}"));

            Console.ReadKey();
        }

        private static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
	    // Add this line:
	    .WriteTo.File(
	    	@"C:\temp\LogFiles\ToDoApp\log.txt",
		fileSizeLimitBytes: 10_000_000,
		rollOnFileSizeLimit: true,
		shared: true,
		flushToDiskInterval: TimeSpan.FromSeconds(1))
            .CreateLogger();

        }
    }
}

