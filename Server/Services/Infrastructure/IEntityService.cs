using Business.Dto;

namespace Services.Infrastructure;

public interface IEntityService<TDto>
{
    Task<int?> Add(TDto dto);

    Task<IList<TDto>> Get(IList<StringParamDto>? stringParams, IList<NumberParamDto>? numberParams, IList<DateParamDto>? dateParams);

    Task<TDto> Update(TDto dto);

    Task Delete(int id);
}
