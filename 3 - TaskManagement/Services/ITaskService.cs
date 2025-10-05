using TaskManagement.Models;

namespace TaskManagement.Services
{
    public interface ITaskService
    {
        bool AddTask(string title, string description);
        IEnumerable<TaskItem> GetAllTasks();
        bool ToggleTaskCompletion(int id);
        bool DeleteTask(int id); 
        TaskItem? GetTaskById(int id);
    }
}
