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
public sealed class SCartProjectionSpec : BaseSpec<SCartProjectionSpec, ShopCart, CartDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<ShopCart, CartDTO>> Spec => e => new()
    {
        Id = e.Id,
        Price = e.Price,
        Count = e.Count,
       
        UserId = e.UserId
    };

    public SCartProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public SCartProjectionSpec(Guid id) : base(id)
    {
    }

    public SCartProjectionSpec(Guid id,Guid UserId)
    {
        if (id == UserId)
        {

        }
        Query.Where(e => e.UserId == UserId);
    }

    public SCartProjectionSpec(Guid UserId,bool state)
    {
        if (state == true)
        {
            Query.Where(e => e.UserId == UserId && e.InUse == state);
        }
        else
        {
            Query.Where(e => e.UserId == UserId);
        }
    }
}
