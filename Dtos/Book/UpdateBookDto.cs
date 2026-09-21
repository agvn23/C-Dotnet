using System.ComponentModel.DataAnnotations;

namespace OneDayApi.Dtos.Book;

public class UpdateBookDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Range(1000, 2100)]
    public int Year { get; set; }

    [Required]
    public Guid AuthorId { get; set; }
}