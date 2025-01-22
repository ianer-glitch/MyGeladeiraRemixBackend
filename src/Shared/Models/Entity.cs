namespace Models;

public class Entity : IEntity,IId
{
    public Guid Id { get; set; }
    public DateTime Inclusion { get; set; }
    public DateTime? Modified { get; set; }
    public bool IsActive { get; set; }
    public Guid UserInclusionId { get; set; }
    public Guid UserModifiedId { get; set; }

    public void SetActive()
    {
        IsActive = true;
        Modified = DateTime.UtcNow;
    }

    public void SetInactive()
    {
        IsActive = false;
        Modified = DateTime.UtcNow;
    }

    public bool Equals<TEntity>(TEntity other) where TEntity : IEntity
    {
        throw new NotImplementedException();
    }


}