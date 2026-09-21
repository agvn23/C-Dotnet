using OneDayApi.Application.Interfaces;
using OneDayApi.Dtos.Author;
using OneDayApi.Models;

namespace OneDayApi.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly Dictionary<Guid, Author> _authors = new();

    public IEnumerable<AuthorResponseDto> GetAll() =>
        _authors.Values.Select(ToDto);

    public AuthorResponseDto? GetById(Guid id) =>
        _authors.TryGetValue(id, out var a) ? ToDto(a) : null;

    public AuthorResponseDto Create(CreateAuthorDto dto)
    {
        var author = new Author { Id = Guid.NewGuid(), Name = dto.Name, Bio = dto.Bio };
        _authors[author.Id] = author;
        return ToDto(author);
    }

    public AuthorResponseDto? Update(Guid id, UpdateAuthorDto dto)
    {
        if (!_authors.TryGetValue(id, out var author)) return null;
        author.Name = dto.Name;
        author.Bio = dto.Bio;
        return ToDto(author);
    }

    public bool Delete(Guid id) => _authors.Remove(id);

    public bool Exists(Guid id) => _authors.ContainsKey(id);

    private static AuthorResponseDto ToDto(Author a) =>
        new() { Id = a.Id, Name = a.Name, Bio = a.Bio };
}