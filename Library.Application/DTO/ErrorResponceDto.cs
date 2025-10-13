namespace Library.BusinessLogic.DTO
{
    public class ErrorResponceDto
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string? Details { get; set; }
    }
}
