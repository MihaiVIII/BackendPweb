using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a specification to filter the user entities and map it to and UserDTO object via the constructors.
/// Note how the constructors call the base class's constructors. Also, this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class AddrFProjectionSpec : BaseSpec<AddrFProjectionSpec, AdreseFacturare, AddressDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<AdreseFacturare, AddressDTO>> Spec => e => new()
    {
        Id = e.Id,
        City = e.City,
        Street = e.Street,
        SNumber = e.SNumber,
        Scara = e.Scara,
        Bloc = e.Bloc,
        Apartament = e.Apartament,
        Description = e.Description,

        UserId = e.UserId
    };

    public AddrFProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public AddrFProjectionSpec(Guid id) : base(id)
    {
    }

    public AddrFProjectionSpec(Guid id,Guid UserId)
    {
        if (id == UserId)
        {

        }
        Query.Where(e => e.UserId == UserId);
    }

    public AddrFProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.City, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
                                                                                           // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
