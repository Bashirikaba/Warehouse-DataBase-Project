namespace Business.Dto;

/// <summary>
/// Склад
/// </summary>
public class WarehouseDto
{
    public int? Id { get; set; }

    public string? ManagerTIN { get; set; }

    public required string Name { get; set; }
}
