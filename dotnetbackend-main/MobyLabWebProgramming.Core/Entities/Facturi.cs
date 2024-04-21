using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for a user entity, it will be mapped to a single table and each property will have it's own column except for entity object references also known as navigation properties.
/// </summary>
public class Facturi : BaseEntity
{
    public int Pret { get; set; } = default!;
    public DateTime ShippingTime { get; set; } = default!;
    public FacturiStateEnum State { get; set; } = FacturiStateEnum.Processing;
    public Guid IdAdd { get; set; }
    public AdreseFacturare Address { get; set; } = default!;
    public Guid CartId { get; set; }
    public ShopCart Cart { get; set; } = default!;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;


}
