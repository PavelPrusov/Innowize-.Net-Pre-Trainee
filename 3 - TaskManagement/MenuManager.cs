using TaskManagement.Services;

class MenuManager
{
    private readonly ITaskService _taskService;

    public MenuManager(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
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

    private void AddTask()
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

    private void ViewTasks()
    {
        var tasks = _taskService.GetAllTasks().ToList();
        foreach (var task in tasks)
            Console.WriteLine($"{task.Id}: {task.Title} - {task.Description} | Completed: {task.IsCompleted}");

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private void ToggleTaskCompletion()
    {
        Console.Write("Task Id to toggle: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            if (_taskService.ToggleTaskCompletion(id))
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

    private void DeleteTask()
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
