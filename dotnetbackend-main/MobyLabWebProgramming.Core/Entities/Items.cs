namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for a user entity, it will be mapped to a single table and each property will have it's own column except for entity object references also known as navigation properties.
/// </summary>
public class Items : BaseEntity
{
    public string Name { get; set; } = default!;
    public int Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;

    public Guid UserId { get; set; }
    public User Producer { get; set; } = default!;

    public ICollection<Item_In_Carts> References { get; set; } = default!;

}
