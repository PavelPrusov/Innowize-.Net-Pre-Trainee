namespace Library.BusinessLogic.DTO.Book
{
    public class BookFilterDto
    {
        public int? AuthorId { get; set; }
        public int? PublishedAfter { get; set; }
        public string? TitlePart { get; set; }
    }
}
