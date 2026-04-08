using Business.Enums;

namespace Business.Dto;

public class NumberParamDto
{
    public required string Field { get; set; }

    public decimal? Value { get; set; }

    public SearchOperations? Operation { get; set; }

    public bool? IsAscendingSort { get; set; }
}
