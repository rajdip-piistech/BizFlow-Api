namespace BizFlow.Domain.Extensions.Results;

public class CommandResult<T>
{
    public CommandResult() { }
    public CommandResult(T result, CommandResultTypeEnum type)
    {
        Result = result;
        Type = type;
    }
    public T Result { get; set; }
    public CommandResultTypeEnum Type { get; set; }
}
public enum CommandResultTypeEnum
{
    Success = 200,
    Created = 201,
    BadRequest = 400,
    Unauthorized = 401,
    NotFound = 404,
    Conflict = 409,
    UnprocessableEntity = 500
}
