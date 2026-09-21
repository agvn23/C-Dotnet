using OneDayApi.Dtos.Book;

namespace OneDayApi.Application.Interfaces;

public interface IBookService
{
    IEnumerable<BookResponseDto> GetAll();
    IEnumerable<BookResponseDto> GetByAuthor(Guid authorId);
    BookResponseDto? GetById(Guid id);
    BookResponseDto Create(CreateBookDto dto);
    BookResponseDto? Update(Guid id, UpdateBookDto dto);
    bool Delete(Guid id);
}