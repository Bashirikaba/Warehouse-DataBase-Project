using Business.Dto.Search;

namespace Services.Infrastructure;

public interface IEntityService<TDto>
{
    Task<int?> Add(TDto dto);

    Task<IReadOnlyList<TDto>> Get(SearchDataDto? dto);

    Task<TDto> Update(TDto dto);

    Task Delete(int id);
}
