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
public class InvoicesService : IEntityService<InvoiceDto>
{
    private IUnitOfWork _unitOfWork;

    private IRepository<Invoice> _invoiceRepository;

    public InvoicesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _invoiceRepository = _unitOfWork.GetRepository<Invoice>();
    }

    public async Task<int?> Add(InvoiceDto dto)
    {
        _unitOfWork.BeginTransaction();

        Warehouse? warehouse = await _unitOfWork.GetRepository<Warehouse>().GetByIdAsync(dto.Warehouse.Id);
        Good? good = await _unitOfWork.GetRepository<Good>().GetByIdAsync(dto.Good.Id);

        if (warehouse is null || good is null) return 0;

        Invoice invoice = new()
        {
            Warehouse = warehouse,
            Good = good,
            InvoiceNumber = dto.InvoiceNumber,
            Date = dto.Date,
            RouteType = dto.RouteType,
            Quantity = dto.Quantity,
            Cost = dto.Cost,
        };

        object? id = await _invoiceRepository.InsertAsync(invoice);
        await _unitOfWork.CommitAsync();

        return Convert.ToInt32(id);
    }

    public async Task Delete(int id)
    {
        _unitOfWork.BeginTransaction();
        await _invoiceRepository.DeleteByIdAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IReadOnlyList<InvoiceDto>> Get(SearchDataDto? dto)
    {
        IQueryable<Invoice> query = _invoiceRepository.Query();

        if (dto != null)
        {
            if (dto.StringParams != null) query = query.ApplyStringFilters(dto.StringParams);
            if (dto.NumberParams != null) query = query.ApplyNumberFilters(dto.NumberParams);
            if (dto.DateParams != null) query = query.ApplyDateFilters(dto.DateParams);
            if (dto.SortExpression != null) query = query.ApplySorting(dto.SortExpression);
        }

        return await query.Select(i => new InvoiceDto
        {
            Id = i.Id,
            Warehouse = i.Warehouse,
            Good = i.Good,
            InvoiceNumber = i.InvoiceNumber,
            Date = i.Date,
            RouteType = i.RouteType,
            Quantity = i.Quantity,
            Cost = i.Cost,
        }).ToListAsync();
    }

    public async Task<InvoiceDto> Update(InvoiceDto dto)
    {
        if (dto.Id is not null)
        {
            Warehouse? warehouse = await _unitOfWork.GetRepository<Warehouse>().GetByIdAsync(dto.Warehouse.Id);
            Good? good = await _unitOfWork.GetRepository<Good>().GetByIdAsync(dto.Good.Id);

            Invoice invoice = new()
            {
                Id = Convert.ToInt32(dto.Id),
                Warehouse = warehouse,
                Good = good,
                InvoiceNumber = dto.InvoiceNumber,
                Date = dto.Date,
                RouteType = dto.RouteType,
                Quantity = dto.Quantity,
                Cost = dto.Cost,
            };
            _unitOfWork.BeginTransaction();

            await _invoiceRepository.UpdateAsync(invoice);
            await _unitOfWork.CommitAsync();
        }

        return dto;
    }
}
