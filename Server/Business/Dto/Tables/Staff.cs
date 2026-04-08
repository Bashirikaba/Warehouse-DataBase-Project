namespace Business.Dto;

/// <summary>
/// Персонал
/// </summary>
public class StaffDto
{
    public int? Id { get; set; }

    public int WarehouseId { get; set; }

    public int PositionId { get; set; }

    public required string FullName { get; set; }

    public required string TIN { get; set; }
}
