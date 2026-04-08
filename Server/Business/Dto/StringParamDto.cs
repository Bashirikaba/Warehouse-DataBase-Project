namespace Business.Dto;

public class StringParamDto
{
    public required string Field { get; set; }

    public string? Value { get; set; }
    
    public bool? IsEqual { get; set; }

    public bool? IsAscendingSort { get; set; }
}
