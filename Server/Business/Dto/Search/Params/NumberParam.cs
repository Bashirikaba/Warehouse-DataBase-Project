using Business.Enums;

namespace Business.Dto.Search.Params;

public class NumberParam
{
    public required string Field { get; set; }

    public decimal Value { get; set; }

    public SearchOperations Operation { get; set; }
}
