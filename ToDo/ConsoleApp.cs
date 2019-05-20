using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ToDo
{
    public class ConsoleApp
    {
        private static ConsoleApp _instance;
        private ToDoManager _todoManager;

        private ConsoleApp()
        {
            Init();
        }

        public static ConsoleApp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConsoleApp();
                return _instance;
            }
        }

        private void Init()
        {
            var serviceCollection = new ServiceCollection();

            ConfigureSerilog();

            serviceCollection.AddLogging(configure => configure.AddSerilog());
            serviceCollection.AddScoped<ToDoContext>();

            serviceCollection.AddScoped<ToDoManager>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _todoManager = serviceProvider.GetService<ToDoManager>();

        } 

        private void ConfigureSerilog()
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

        public Command ReadCommand()
        {
            Console.WriteLine("Please type your command:");
            var cmdStr = Console.ReadLine();

            return Command.Parse(cmdStr);
        }

        public void ExecuteComand(Command command)
        {
            switch (command.CommandType)
            {
                case CommandType.Help:
                    Enum.GetNames(typeof(CommandType)).ToList().ForEach(name => Console.WriteLine(name));
                    break;
                case CommandType.Delete:
                    if(command.Parameters.Any())
                    {
                        try
                        {
                            _todoManager.DeleteToDoItem(new Guid(command.Parameters.First()));

                        }
                        catch
                        { }
                    }
                    break;
                case CommandType.Update:
                    if(command.Parameters.Count == 2)
                    {
                        try
                        {
                            _todoManager.UpdateToDoItem(new ToDoItem(new Guid(command.Parameters[0]), command.Parameters[1]));

                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    break;
                    
                case CommandType.Complete:
                    _todoManager.CompleteTodoItem(new Guid(command.Parameters[0]));
                    break;
                case CommandType.List:
                   var list = _todoManager.ToDoList;
                    list.ForEach(l => Console.WriteLine(l));
                    break;

                case CommandType.Add:
                    if (command.Parameters.Any())
                        _todoManager.AddToDoItem(new ToDoItem(command.Parameters[0]));
                    break;
                 

                default:
                    break;
                    
            }
        }
    }
    public class Command
    {
        public Command( CommandType type, List<string> parameters)
        {
            CommandType = type;
            Parameters = parameters;

        }
        public CommandType CommandType{ get; set; }
        public List<string> Parameters { get; set; }


        public override string ToString()
        {
            return $"[{CommandType.ToString()}]:{String.Join(" ",Parameters)}";
        }

        public static Command Parse(string command)
        {
            var words = command.Split(" ").ToList();
            words = words.FindAll(x => x.Trim() != String.Empty);


            CommandType cmd;
            var result = Enum.TryParse<CommandType>(words[0], out cmd);

            if (result)
                return new Command(cmd, words.Skip(1).ToList());
            else
                return null;
        }
    }

    public enum CommandType
    {
        Help,
        List,
        Add,
        Delete,
        Update,
        Complete
    }
}

