using Business.Enums;

namespace Business.Dto.Search.Params;

public class DateParam
{
    public required string Field { get; set; }

    public DateTime Value { get; set; }

    public SearchOperations Operation { get; set; }
}
