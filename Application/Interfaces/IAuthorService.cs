using OneDayApi.Dtos.Author;

namespace OneDayApi.Application.Interfaces;

public interface IAuthorService
{
    IEnumerable<AuthorResponseDto> GetAll();
    AuthorResponseDto? GetById(Guid id);
    AuthorResponseDto Create(CreateAuthorDto dto);
    AuthorResponseDto? Update(Guid id, UpdateAuthorDto dto);
    bool Delete(Guid id);
    bool Exists(Guid id);
}