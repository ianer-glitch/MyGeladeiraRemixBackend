namespace Models;

public interface IEntity
{
    public DateTime Inclusion { get; set; }
    public DateTime? Modified { get; set; }
    public bool IsActive { get; set; }
    public Guid UserInclusionId { get; set; }
    public Guid UserModifiedId { get; set; }    
    
    public void SetActive();
    public void SetInactive();
    public bool Equals<TEntity>(TEntity other) where TEntity : IEntity  ;

}