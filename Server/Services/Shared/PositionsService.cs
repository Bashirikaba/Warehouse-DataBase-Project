using ApplicationData.Infrastructure;
using ApplicationData.Shared.Helpers;
using Business.Attributes;
using Business.Dto;
using Business.Dto.Search;
using Business.Models;
using NHibernate.Linq;
using Services.Infrastructure;

namespace Services.Shared;

[AutoRoute]
public class PositionsService : IEntityService<PositionDto>
{
    private IUnitOfWork _unitOfWork;

    private IRepository<Position> _positionRepository;

    public PositionsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _positionRepository = _unitOfWork.GetRepository<Position>();
    }

    public async Task<int?> Add(PositionDto dto)
    {
        _unitOfWork.BeginTransaction();

        Position position = new() { Name = dto.Name };

        object? id = await _positionRepository.InsertAsync(position);
        await _unitOfWork.CommitAsync();

        return Convert.ToInt32(id);
    }

    public async Task Delete(int id)
    {
        _unitOfWork.BeginTransaction();
        await _positionRepository.DeleteByIdAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IReadOnlyList<PositionDto>> Get(SearchDataDto? dto)
    {
        IQueryable<Position> query = _positionRepository.Query();

        if (dto != null && dto.StringParams != null)
        {
            query = query.ApplyStringFilters(dto.StringParams);
        }

        return await query.Select(p => new PositionDto { Id = p.Id, Name = p.Name }).ToListAsync();
    }

    public async Task<PositionDto> Update(PositionDto dto)
    {
        if (dto.Id is not null)
        {
            Position position = new() { Id = Convert.ToInt32(dto.Id), Name = dto.Name };
            _unitOfWork.BeginTransaction();

            await _positionRepository.UpdateAsync(position);
            await _unitOfWork.CommitAsync();
        }

        return dto;
    }
}
