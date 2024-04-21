using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record FacturiUpdateDTO
(
    Guid Id,
    DateTime? ShippingTime = default,
    FacturiStateEnum? State = default

);
