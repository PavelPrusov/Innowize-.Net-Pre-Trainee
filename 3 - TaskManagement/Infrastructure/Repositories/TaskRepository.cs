using Dapper;
using TaskManagement.Infrastructure.Database;
using TaskManagement.Models;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : IRepository<TaskItem>
    {
        private readonly IConnectionFactory _connectionFactory;

        public TaskRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            EnsureDatabaseCreated();
        }

        public void EnsureDatabaseCreated()
        {
            using var conn = _connectionFactory.CreateConnection();
            const string sql = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Tasks' and xtype='U')
                CREATE TABLE Tasks (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Title NVARCHAR(255) NOT NULL,
                    Description NVARCHAR(MAX),
                    IsCompleted BIT NOT NULL DEFAULT 0,
                    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
                )";
            conn.Execute(sql);
        }


        public IEnumerable<TaskItem> GetAll()
        {
            using var conn = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM Tasks ORDER BY CreatedAt DESC";

            var result = conn.Query<TaskItem>(sql);
            return result; 
        }

        public TaskItem? GetById(int id)
        {
            using var conn = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM Tasks WHERE Id = @Id";

            var result = conn.QuerySingleOrDefault<TaskItem>(sql, new { Id = id });
            return result;
        }

        public bool Add(TaskItem item)
        {
            using var conn = _connectionFactory.CreateConnection();
            const string sql = @"
                INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt)
                VALUES (@Title, @Description, @IsCompleted, @CreatedAt);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            var affected = conn.Execute(sql,item) > 0;
            return affected;
        }

        public bool Update(TaskItem item)
        {
            using var conn = _connectionFactory.CreateConnection();
            const string sql = @"
                UPDATE Tasks
                SET Title = @Title, Description = @Description, IsCompleted = @IsCompleted
                WHERE Id = @Id;
            ";
            var affected = conn.Execute(sql, item) > 0;
            return affected;
        }

        public bool Delete(int id)
        {
            using var conn = _connectionFactory.CreateConnection();
            const string sql = "DELETE FROM Tasks WHERE Id = @Id";

            var affected = conn.Execute(sql, new { Id = id }) > 0;
            return affected;
        }


    }
}
