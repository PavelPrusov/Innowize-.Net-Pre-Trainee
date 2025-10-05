namespace TaskManagement.Models
{
    public class TaskItem
    {
        int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
