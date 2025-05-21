namespace BizFlow.Application.Model.BaseEntities;
/// <summary>
/// AuditableEntity
/// </summary>
/// <typeparam name="TId"></typeparam>
public class AuditableEntity<TId> : BaseEntity<TId>
{
    public AuditableEntity() => IsDelete = false;
    public DateTimeOffset CreatedDate { get; set; }
    public long CreatedBy { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public long? ModifiedBy { get; set; }
    public bool IsDelete { get; set; }
}
public class AuditableEntity : AuditableEntity<long> { }