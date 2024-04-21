namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for a user entity, it will be mapped to a single table and each property will have it's own column except for entity object references also known as navigation properties.
/// </summary>
public class Item_In_Carts : BaseEntity
{
    public int Quantity { get; set; } = default!;
    public Guid ItemId { get; set; }
    public Items Item { get; set; } = default!;
    public Guid CartId { get; set; }
    public ShopCart Cart { get; set; } = default!;


}
