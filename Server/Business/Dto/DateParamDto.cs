using Business.Enums;

namespace Business.Dto;

public class DateParamDto
{
    public required string Field { get; set; }

    public DateTime? Value { get; set; }

    public SearchOperations? Operation { get; set; }

    public bool? IsAscendingSort { get; set; }
}
