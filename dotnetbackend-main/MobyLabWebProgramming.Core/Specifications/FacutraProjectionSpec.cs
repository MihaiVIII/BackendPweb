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
public sealed class FacturaProjectionSpec : BaseSpec<FacturaProjectionSpec, Facturi, FacturiDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<Facturi, FacturiDTO>> Spec => e => new()
    {
        Id = e.Id,
        Pret = e.Pret,
        CartId = e.CartId,
        IdAdd = e.IdAdd,
        UserId = e.UserId,
        State = e.State,
        ShippingTime = e.ShippingTime,
        Date = e.CreatedAt
    };

    public FacturaProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public FacturaProjectionSpec(Guid id) : base(id)
    {
    }

    public FacturaProjectionSpec(Guid id,Guid UserId)
    {
        if (id == UserId)
        {

        }
        Query.Where(e => e.UserId == UserId);
    }

}
