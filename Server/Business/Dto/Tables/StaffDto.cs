namespace Business.Dto;

/// <summary>
/// Персонал
/// </summary>
public class StaffDto
{
    public int? Id { get; set; }

    public required string WarehouseName { get; set; }

    public required string PositionName { get; set; }

    public required string FullName { get; set; }

    public required string TIN { get; set; }
}
