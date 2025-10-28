namespace Library.BusinessLogic.DTO.Author
{
    public class AuthorFilterDto
    {
        public string? NamePart { get; set; }
        public bool? HasBooks { get; set; }
        public int? MinBookCount { get; set; }
    }
}
