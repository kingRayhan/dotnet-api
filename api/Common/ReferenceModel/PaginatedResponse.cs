namespace api.Common.ReferenceModel;


public class PaginationMetadata
{
    public int Total { get; set; }
    public int Limit { get; set; }
    public int Page { get; set; }
}
public class PaginatedResponse<T>
{
    public PaginationMetadata Metadata { get; set; }
    public List<T>? Items { get; set; }
}