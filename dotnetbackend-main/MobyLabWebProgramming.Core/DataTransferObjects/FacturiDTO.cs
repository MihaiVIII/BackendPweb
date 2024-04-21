using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FacturiDTO
{
    public Guid Id { get; set; }
    public int Pret { get; set; } = default!;
    public DateTime ShippingTime { get; set; } = default!;
    public FacturiStateEnum State { get; set; } = FacturiStateEnum.Processing;
    public Guid IdAdd { get; set; }
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; } = default!;

}
