using Business.Dto.Search;
using Business.Models;

namespace Services.Infrastructure;

public interface IReportService
{
    Task<IReadOnlyList<ReorderGoodsReport>> GetReorderGoods(SearchDataDto? dto);

    Task<IReadOnlyList<StaffPerformanceReport>> GetStaffPerformance(SearchDataDto? dto);

    Task<IReadOnlyList<WarehousePeriodReport>> GetWarehousePeriod(SearchDataDto? dto);
}
