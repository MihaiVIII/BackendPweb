using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FeedbackDTO
{
    public int Score { get; set; } = 0;
    public bool Anonimous { get; set; } = false;
    public FeedbackEnum Quality { get; set; } = FeedbackEnum.Decent;
    public string Content { get; set; } = default!;
    public Guid? UserId { get; set; } = default!;
    public DateTime? Date { get; set; } = default!;

}
