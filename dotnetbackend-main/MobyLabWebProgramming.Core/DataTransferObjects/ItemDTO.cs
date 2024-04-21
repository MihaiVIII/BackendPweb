namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ItemDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public Guid UserId { get; set; }

}
