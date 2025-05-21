using BizFlow.Application.Model.BaseEntities;
namespace BizFlow.Domain.Model.BaseEntities;
public class MasterEntity:BaseEntity
{
    public MasterEntity()
    {
        this.CreatedBy = 1;
        this.CreatedDate = DateTimeOffset.Now;
        this.IsDelete = false;
    }

    public long CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
    public bool IsDelete { get; set; }
}
