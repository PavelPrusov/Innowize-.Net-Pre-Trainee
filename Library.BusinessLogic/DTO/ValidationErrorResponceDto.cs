namespace Library.BusinessLogic.DTO
{
    public record ValidationErrorResponceDto(
        int StatusCode,
        string Title,
        string Message,
        Dictionary<string, string[]> Errors
    ) : ErrorResponceDto(StatusCode, Title, Message);
}
