using Models;

namespace Fridge.Domain.Fridges;

public class Fridge : Entity
{
    public Fridge()
    {
        
    }

    public Fridge(Guid userId,Guid userInclusionId) : base(userInclusionId)
    {
        UserId = userId;
    }
    public Guid UserId { get; set; }
}