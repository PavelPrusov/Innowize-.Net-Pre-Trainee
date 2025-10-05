using TaskManagement.Infrastructure.Repositories;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class TaskService: ITaskService
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public TaskService(IRepository<TaskItem> repository)
        {
            _taskRepository = repository;
        }

        public bool AddTask(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title)) return false;

            var task = new TaskItem
            {
                Title = title,
                Description = description,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            return _taskRepository.Add(task);
        }

        public IEnumerable<TaskItem> GetAllTasks() => _taskRepository.GetAll();

        public bool ToggleTaskCompletion(int id)
        {
            var task = _taskRepository.GetById(id);
            if (task == null) return false;

            task.IsCompleted = !task.IsCompleted;
            return _taskRepository.Update(task);
        }

        public bool DeleteTask(int id) => _taskRepository.Delete(id);

        public TaskItem? GetTaskById(int id)
        {
            var task = _taskRepository.GetById(id);
            return task;
        }
       
    }
}
