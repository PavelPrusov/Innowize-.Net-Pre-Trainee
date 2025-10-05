using Microsoft.Extensions.Configuration;
using TaskManagement.Infrastructure.Database;
using TaskManagement.Infrastructure.Repositories;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement
{
    class Program
    {
        private static ITaskService _taskService;

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionFactory = new ConnectionFactory(configuration);
            var repository = new TaskRepository(connectionFactory);
            _taskService = new TaskService(repository);

            RunMenu();
        }

        private static void RunMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Task Manager ===");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View All Tasks");
                Console.WriteLine("3. Toggle Task Completion");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddTask(); break;
                    case "2": ViewTasks(); break;
                    case "3": ToggleTaskCompletion(); break;
                    case "4": DeleteTask(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void AddTask()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine()!;
            Console.Write("Description: ");
            var description = Console.ReadLine()!;

            if (_taskService.AddTask(title, description))
                Console.WriteLine("Task added successfully.");
            else
                Console.WriteLine("Failed to add task.");

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void ViewTasks()
        {
            List<TaskItem> tasks = _taskService.GetAllTasks().ToList();
            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                Console.WriteLine($"{task.Id}: {task.Title} - {task.Description} | Completed: {task.IsCompleted}");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void ToggleTaskCompletion()
        {
            Console.Write("Task num to toggle: ");
            if (int.TryParse(Console.ReadLine(), out var num))
            {
                if (_taskService.ToggleTaskCompletion(num))
                    Console.WriteLine("Task updated successfully.");
                else
                    Console.WriteLine("Task not found.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void DeleteTask()
        {
            Console.Write("Task Id to delete: ");
            if (int.TryParse(Console.ReadLine(), out var id))
            {
                if (_taskService.DeleteTask(id))
                    Console.WriteLine("Task deleted successfully.");
                else
                    Console.WriteLine("Task not found.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
