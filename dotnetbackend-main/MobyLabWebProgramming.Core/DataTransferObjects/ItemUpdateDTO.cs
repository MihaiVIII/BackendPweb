namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record ItemUpdateDTO
(
    Guid Id,
    Guid Producer,
    string? Name = default,
    int? Price = default,
    int? Quantity = default

);
