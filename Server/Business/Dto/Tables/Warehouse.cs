namespace Business.Dto;

/// <summary>
/// Склад
/// </summary>
public class WarehouseDto
{
    public int? Id { get; set; }

    public int? ManagerId { get; set; }

    public required string Name { get; set; }
}
