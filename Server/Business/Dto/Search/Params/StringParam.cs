using Business.Enums;

namespace Business.Dto.Search.Params;

public class StringParam
{
    public required string Field { get; set; }

    public required string Value { get; set; }
    
    public SearchOperations Operation { get; set; }
}
