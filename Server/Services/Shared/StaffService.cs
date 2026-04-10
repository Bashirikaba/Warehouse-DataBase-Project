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
public class StaffService : IEntityService<StaffDto>
{
    private IUnitOfWork _unitOfWork;

    private IRepository<Staff> _staffRepository;

    public StaffService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _staffRepository = _unitOfWork.GetRepository<Staff>();
    }

    public async Task<int?> Add(StaffDto dto)
    {
        _unitOfWork.BeginTransaction();

        Warehouse? warehouse = _unitOfWork.GetRepository<Warehouse>().GetByFieldAsync("Name", dto.WarehouseName);
        Position? position = _unitOfWork.GetRepository<Position>().GetByFieldAsync("Name", dto.PositionName);

        if (warehouse is null || position is null) return 0;

        Staff staff = new()
        {
            Warehouse = warehouse,
            Position = position,
            FullName = dto.FullName,
            TIN = dto.TIN,
        };

        object? id = await _staffRepository.InsertAsync(staff);
        await _unitOfWork.CommitAsync();

        return Convert.ToInt32(id);
    }

    public async Task Delete(int id)
    {
        _unitOfWork.BeginTransaction();
        await _staffRepository.DeleteByIdAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IReadOnlyList<StaffDto>> Get(SearchDataDto? dto)
    {
        IQueryable<Staff> query = _staffRepository.Query();

        if (dto != null && dto.StringParams != null)
        {
            query = query.ApplyStringFilters(dto.StringParams);
        }

        return await query.Select(i => new StaffDto
        {
            Id = i.Id,
            WarehouseName = i.Warehouse.Name,
            PositionName = i.Position.Name,
            FullName = i.FullName,
            TIN = i.TIN,
        }).ToListAsync();
    }

    public async Task<StaffDto> Update(StaffDto dto)
    {
        if (dto.Id is not null)
        {
            Warehouse? warehouse = _unitOfWork.GetRepository<Warehouse>().GetByFieldAsync("Name", dto.WarehouseName);
            Position? position = _unitOfWork.GetRepository<Position>().GetByFieldAsync("Name", dto.PositionName);

            if (warehouse is null)
            {
                dto.WarehouseName = "";
                return dto;
            }

            if (position is null)
            {
                dto.PositionName = "";
                return dto;
            }

            Staff staff = new()
            {
                Id = Convert.ToInt32(dto.Id),
                Warehouse = warehouse,
                Position = position,
                FullName = dto.FullName,
                TIN = dto.TIN,
            };
            _unitOfWork.BeginTransaction();

            await _staffRepository.UpdateAsync(staff);
            await _unitOfWork.CommitAsync();
        }

        return dto;
    }
}
