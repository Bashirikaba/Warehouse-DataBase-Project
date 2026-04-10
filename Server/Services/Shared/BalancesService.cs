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
public class BalancesService : IEntityService<BalanceDto>
{
    private IUnitOfWork _unitOfWork;

    private IRepository<Balance> _balanceRepository;

    public BalancesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _balanceRepository = _unitOfWork.GetRepository<Balance>();
    }

    public async Task<int?> Add(BalanceDto dto)
    {
        _unitOfWork.BeginTransaction();

        Warehouse? warehouse = _unitOfWork.GetRepository<Warehouse>().GetByFieldAsync("Name", dto.WarehouseName);
        Good? good = _unitOfWork.GetRepository<Good>().GetByFieldAsync("NomenclatureNumber", dto.GoodNomenclatureNumber);

        if (warehouse is null || good is null) return 0;

        Balance balance = new()
        {
            Warehouse = warehouse,
            Good = good,
            Quantity = dto.Quantity,
        };

        object? id = await _balanceRepository.InsertAsync(balance);
        await _unitOfWork.CommitAsync();

        return Convert.ToInt32(id);
    }

    public async Task Delete(int id)
    {
        _unitOfWork.BeginTransaction();
        await _balanceRepository.DeleteByIdAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IReadOnlyList<BalanceDto>> Get(SearchDataDto? dto)
    {
        IQueryable<Balance> query = _balanceRepository.Query();

        if (dto != null)
        {
            if (dto.StringParams != null) query = query.ApplyStringFilters(dto.StringParams);
            if (dto.NumberParams != null) query = query.ApplyNumberFilters(dto.NumberParams);
        }

        return await query.Select(i => new BalanceDto
        {
            Id = i.Id,
            WarehouseName = i.Warehouse.Name,
            GoodNomenclatureNumber = i.Good.NomenclatureNumber,
            Quantity = i.Quantity,
        }).ToListAsync();
    }

    public async Task<BalanceDto> Update(BalanceDto dto)
    {
        if (dto.Id is not null)
        {
            Warehouse? warehouse = _unitOfWork.GetRepository<Warehouse>().GetByFieldAsync("Name", dto.WarehouseName);
            Good? good = _unitOfWork.GetRepository<Good>().GetByFieldAsync("NomenclatureNumber", dto.GoodNomenclatureNumber);

            if (warehouse is null)
            {
                dto.WarehouseName = "";
                return dto;
            }

            if (good is null)
            {
                dto.GoodNomenclatureNumber = "";
                return dto;
            }

            Balance balance = new()
            {
                Id = Convert.ToInt32(dto.Id),
                Warehouse = warehouse,
                Good = good,
                Quantity = dto.Quantity,
            };
            _unitOfWork.BeginTransaction();

            await _balanceRepository.UpdateAsync(balance);
            await _unitOfWork.CommitAsync();
        }

        return dto;
    }
}
