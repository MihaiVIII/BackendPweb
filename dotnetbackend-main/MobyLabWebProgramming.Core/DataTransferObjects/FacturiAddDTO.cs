using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FacturiAddDTO
{
    public Guid IdAdd { get; set; }
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }

}
