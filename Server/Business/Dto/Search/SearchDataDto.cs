using Business.Dto.Search.Params;

namespace Business.Dto.Search;

public class SearchDataDto
{
    public StringParam[]? StringParams { get; set; }
    
    public NumberParam[]? NumberParams { get; set; }
    
    public DateParam[]? DateParams { get; set; }
}
