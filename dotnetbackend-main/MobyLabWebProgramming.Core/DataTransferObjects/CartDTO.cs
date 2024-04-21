namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CartDTO
{
    public Guid Id { get; set; }
    public int Count { get; set; } = default!;
    public int Price { get; set; } = default!;
    public Guid UserId { get; set; }

}
