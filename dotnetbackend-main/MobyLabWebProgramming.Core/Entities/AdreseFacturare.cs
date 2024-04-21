namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for another entity to store files and an example for a One-To-Many relation.
/// </summary>
public class AdreseFacturare : BaseEntity
{
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public int SNumber { get; set; } = default!;

    public int? Scara { get; set; }
    public int? Bloc { get; set; }
    public int? Apartament { get; set; }
    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = default!;

    public ICollection<Facturi> Facturi { get; set; } = default!;
}
