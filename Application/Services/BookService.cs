using OneDayApi.Application.Interfaces;
using OneDayApi.Dtos.Book;
using OneDayApi.Models;

namespace OneDayApi.Application.Services;

public class BookService : IBookService
{
    private readonly Dictionary<Guid, Book> _books = new();

    public IEnumerable<BookResponseDto> GetAll() =>
        _books.Values.Select(ToDto);

    public IEnumerable<BookResponseDto> GetByAuthor(Guid authorId) =>
        _books.Values.Where(b => b.AuthorId == authorId).Select(ToDto);

    public BookResponseDto? GetById(Guid id) =>
        _books.TryGetValue(id, out var b) ? ToDto(b) : null;

    public BookResponseDto Create(CreateBookDto dto)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Year = dto.Year,
            AuthorId = dto.AuthorId
        };
        _books[book.Id] = book;
        return ToDto(book);
    }

    public BookResponseDto? Update(Guid id, UpdateBookDto dto)
    {
        if (!_books.TryGetValue(id, out var book)) return null;
        book.Title = dto.Title;
        book.Year = dto.Year;
        return ToDto(book);
    }

    public bool Delete(Guid id) => _books.Remove(id);

    private static BookResponseDto ToDto(Book b) =>
        new() { Id = b.Id, Title = b.Title, Year = b.Year, AuthorId = b.AuthorId };
}