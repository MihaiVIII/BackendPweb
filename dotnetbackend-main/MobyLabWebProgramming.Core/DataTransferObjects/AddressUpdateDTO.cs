namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record AddressUpdateDTO
(
    Guid Id,
    Guid UserID,
    string? City = default,
    string? Street = default,
    int? SNumber = default,

    int? Scara = default,
    int? Bloc = default,
    int? Apartament = default,
    string? Description = default


);
