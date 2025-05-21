namespace BizFlow.Domain.Extensions.Results;

public class QueryResult<T>
{
    public QueryResult(T result, QueryResultTypeEnum type) { Result = result; Type = type; }
    public T Result { get; set; }
    public QueryResultTypeEnum Type { get; set; }
}
public enum QueryResultTypeEnum
{
    Success,
    InvalidInput,
    UnprocessableEntity,
    NotFound
}
