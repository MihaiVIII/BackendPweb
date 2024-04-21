namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AddressAddDTO
{
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public int SNumber { get; set; } = default!;

    public int? Scara { get; set; }
    public int? Bloc { get; set; }
    public int? Apartament { get; set; }
    public string? Description { get; set; }


}
