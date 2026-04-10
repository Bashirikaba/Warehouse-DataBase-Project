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
public class WarehousesService : IEntityService<WarehouseDto>
{
    private IUnitOfWork _unitOfWork;

    private IRepository<Warehouse> _warehouseRepository;

    public WarehousesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _warehouseRepository = _unitOfWork.GetRepository<Warehouse>();
    }

    public async Task<int?> Add(WarehouseDto dto)
    {
        _unitOfWork.BeginTransaction();

        Staff? staff = _unitOfWork.GetRepository<Staff>().GetByFieldAsync("TIN", dto.ManagerTIN);

        Warehouse? warehouse = new()
        {
            Manager = staff,
            Name = dto.Name,
        };

        object? id = await _warehouseRepository.InsertAsync(warehouse);
        await _unitOfWork.CommitAsync();

        return Convert.ToInt32(id);
    }

    public async Task Delete(int id)
    {
        _unitOfWork.BeginTransaction();
        await _warehouseRepository.DeleteByIdAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IReadOnlyList<WarehouseDto>> Get(SearchDataDto? dto)
    {
        IQueryable<Warehouse> query = _warehouseRepository.Query();

        if (dto != null && dto.StringParams != null)
        {
            query = query.ApplyStringFilters(dto.StringParams);
        }

        return await query.Select(g => new WarehouseDto
        {
            Id = g.Id,
            ManagerTIN = g.Manager != null ? g.Manager.TIN : null,
            Name = g.Name,
        }).ToListAsync();
    }

    public async Task<WarehouseDto> Update(WarehouseDto dto)
    {
        if (dto.Id is not null)
        {
            Staff? staff = _unitOfWork.GetRepository<Staff>().GetByFieldAsync("TIN", dto.ManagerTIN);

            Warehouse warehouse = new()
            {
                Id = Convert.ToInt32(dto.Id),
                Manager = staff,
                Name = dto.Name,
            };
            _unitOfWork.BeginTransaction();

            await _warehouseRepository.UpdateAsync(warehouse);
            await _unitOfWork.CommitAsync();
        }

        return dto;
    }
}
