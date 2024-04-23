using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for a user entity, it will be mapped to a single table and each property will have it's own column except for entity object references also known as navigation properties.
/// </summary>
public class Feedback : BaseEntity
{
    public int Score { get; set; } = 0;
    public bool Anonimous { get; set; } = false;
    public FeedbackEnum Quality { get; set; } = FeedbackEnum.Decent;
    public string Content { get; set; } = default!;
    public Guid? UserId { get; set; }
    public User? User { get; set; } = default!;


}
