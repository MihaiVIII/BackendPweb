namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for a user entity, it will be mapped to a single table and each property will have it's own column except for entity object references also known as navigation properties.
/// </summary>
public class ShopCart : BaseEntity
{
    public int Count { get; set; } = default!;
    public int Price { get; set; } = default!;
    public bool InUse { get; set; } = false;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<Item_In_Carts> Products { get; set; } = default!;
    public Facturi Facturi { get; set; } = default!;
}


