namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ItemInCartDTO
{
    public Guid Id { get; set; }
    public int Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public Guid CartId { get; set; } = default!;
    public Guid ItemID { get; set; } = default!;

}