using Microsoft.Extensions.Configuration;
using TaskManagement.Infrastructure.Database;
using TaskManagement.Infrastructure.Repositories;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            IConnectionFactory connectionFactory = new ConnectionFactory(configuration);
            IRepository<TaskItem> repository = new TaskRepository(connectionFactory);
            ITaskService taskService = new TaskService(repository);

            var menu = new MenuManager(taskService);
            menu.Run();
        }


    }
}
