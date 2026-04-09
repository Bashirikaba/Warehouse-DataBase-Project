using ApplicationData.Infrastructure;
using Business.Attributes;
using Business.Dto;
using Business.Models;
using Services.Infrastructure;

namespace Services.Shared;

[AutoRoute]
public record class PositionsService : IEntityService<PositionDto>
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

        Position? position = new() { Name = dto.Name };

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

    public Task<IList<PositionDto>> Get(IList<StringParamDto>? stringParams, IList<NumberParamDto>? numberParams, IList<DateParamDto>? dateParams)
    {
        throw new NotImplementedException();
    }

    public async Task<PositionDto> Update(PositionDto dto)
    {
        _unitOfWork.BeginTransaction();

        

        await _unitOfWork.CommitAsync();
        throw new NotImplementedException();
    }
}
