using TaskManagement.Services;

class MenuManager
{
    private readonly ITaskService _taskService;

    public MenuManager(ITaskService taskService)
    {
        _taskService = taskService;
    }
    public enum MenuOption
    {
        Exit = 0,
        AddTask = 1,
        ViewTasks = 2,
        ToggleTaskCompletion = 3,
        DeleteTask = 4
    }

    public void Run()
    {
       
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{(int)MenuOption.AddTask}. Add Task");
            Console.WriteLine($"{(int)MenuOption.ViewTasks}. View All Tasks");
            Console.WriteLine($"{(int)MenuOption.ToggleTaskCompletion}. Toggle Task Completion");
            Console.WriteLine($"{(int)MenuOption.DeleteTask}. Delete Task");
            Console.WriteLine($"{(int)MenuOption.Exit}. Exit");
            Console.Write("Choose an option: ");

            MenuOption option = ReadMenuOption();

            switch (option)
            {
                case MenuOption.AddTask: AddTask(); break;
                case MenuOption.ViewTasks: ViewTasks(); break;
                case MenuOption.ToggleTaskCompletion: ToggleTaskCompletion(); break;
                case MenuOption.DeleteTask: DeleteTask(); break;
                case MenuOption.Exit: return;
            }
        }
    }

    private MenuOption ReadMenuOption()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                Enum.IsDefined(typeof(MenuOption), choice))
                return (MenuOption)choice;

            Console.WriteLine("Invalid option. Please enter a valid number:");
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
        var tasks = _taskService.GetAllTasks();

        foreach (var task in tasks)
            Console.WriteLine($"{task.Id}|Title: {task.Title} Description:{task.Description} | Completed: {task.IsCompleted}");

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
