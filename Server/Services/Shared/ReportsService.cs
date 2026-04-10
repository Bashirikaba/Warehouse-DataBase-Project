using ApplicationData.Infrastructure;
using ApplicationData.Shared.Helpers;
using Business.Dto.Search;
using Business.Models;
using NHibernate.Linq;
using Services.Infrastructure;

namespace Services.Shared;

public class ReportsService : IReportService
{
    private IUnitOfWork _unitOfWork;

    public ReportsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ReorderGoodsReport>> GetReorderGoods(SearchDataDto? dto)
    {
        IQueryable<ReorderGoodsReport> query = _unitOfWork.GetReadOnlyRepository<ReorderGoodsReport>().Query();
        if (dto != null)
        {
            if (dto.StringParams != null) query = query.ApplyStringFilters(dto.StringParams);
            if (dto.NumberParams != null) query = query.ApplyNumberFilters(dto.NumberParams);
        }

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<StaffPerformanceReport>> GetStaffPerformance(SearchDataDto? dto)
    {
        IQueryable<StaffPerformanceReport> query = _unitOfWork.GetReadOnlyRepository<StaffPerformanceReport>().Query();
        if (dto != null)
        {
            if (dto.StringParams != null) query = query.ApplyStringFilters(dto.StringParams);
            if (dto.NumberParams != null) query = query.ApplyNumberFilters(dto.NumberParams);
            if (dto.DateParams != null) query = query.ApplyDateFilters(dto.DateParams);
        }

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<WarehousePeriodReport>> GetWarehousePeriod(SearchDataDto? dto)
    {
        IQueryable<WarehousePeriodReport> query = _unitOfWork.GetReadOnlyRepository<WarehousePeriodReport>().Query();
        if (dto != null)
        {
            if (dto.StringParams != null) query = query.ApplyStringFilters(dto.StringParams);
            if (dto.NumberParams != null) query = query.ApplyNumberFilters(dto.NumberParams);
            if (dto.DateParams != null) query = query.ApplyDateFilters(dto.DateParams);
        }

        return await query.ToListAsync();
    }
}
