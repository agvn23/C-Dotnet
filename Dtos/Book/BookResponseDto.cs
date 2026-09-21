namespace OneDayApi.Dtos.Book;

public class BookResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public Guid AuthorId { get; set; }
}