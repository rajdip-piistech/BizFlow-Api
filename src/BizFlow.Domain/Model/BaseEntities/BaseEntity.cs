namespace BizFlow.Application.Model.BaseEntities;

public class BaseEntity<TId>
{
    public TId Id { get; set; }
}
public abstract class BaseEntity : BaseEntity<long> { }
