using System.ComponentModel.DataAnnotations;

namespace OneDayApi.Dtos.Author;

public class CreateAuthorDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Bio { get; set; } = string.Empty;
}