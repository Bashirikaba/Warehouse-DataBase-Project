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
public class GoodsService : IEntityService<GoodDto>
{
    private IUnitOfWork _unitOfWork;

    private IRepository<Good> _goodRepository;

    public GoodsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _goodRepository = _unitOfWork.GetRepository<Good>();
    }

    public async Task<int?> Add(GoodDto dto)
    {
        _unitOfWork.BeginTransaction();

        Good good = new()
        {
            Code = dto.Code,
            NomenclatureNumber = dto.NomenclatureNumber,
            Name = dto.Name,
            Price = dto.Price,
        };

        object? id = await _goodRepository.InsertAsync(good);
        await _unitOfWork.CommitAsync();

        return Convert.ToInt32(id);
    }

    public async Task Delete(int id)
    {
        _unitOfWork.BeginTransaction();
        await _goodRepository.DeleteByIdAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IReadOnlyList<GoodDto>> Get(SearchDataDto? dto)
    {
        IQueryable<Good> query = _goodRepository.Query();

        if (dto != null)
        {
            if (dto.StringParams != null) query = query.ApplyStringFilters(dto.StringParams);
            if (dto.NumberParams != null) query = query.ApplyNumberFilters(dto.NumberParams);
        }

        return await query.Select(g => new GoodDto
        {
            Id = g.Id,
            Code = g.Code,
            NomenclatureNumber = g.NomenclatureNumber,
            Name = g.Name,
            Price = g.Price,
        }).ToListAsync();
    }

    public async Task<GoodDto> Update(GoodDto dto)
    {
        if (dto.Id is not null)
        {
            Good good = new()
            {
                Id = Convert.ToInt32(dto.Id),
                Code = dto.Code,
                NomenclatureNumber = dto.NomenclatureNumber,
                Name = dto.Name,
                Price = dto.Price,
            };
            _unitOfWork.BeginTransaction();

            await _goodRepository.UpdateAsync(good);
            await _unitOfWork.CommitAsync();
        }

        return dto;
    }
}
