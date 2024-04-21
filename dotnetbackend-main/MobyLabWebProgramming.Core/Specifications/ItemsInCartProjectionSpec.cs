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
public sealed class ItemsInCartProjectionSpec : BaseSpec<ItemsInCartProjectionSpec, Item_In_Carts, ItemDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<Item_In_Carts, ItemDTO>> Spec => e => new()
    {
        Id = e.Id,
        Quantity = e.Quantity
    };

    public ItemsInCartProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ItemsInCartProjectionSpec(Guid id) : base(id)
    {
    }

    public ItemsInCartProjectionSpec(Guid id,Guid CardId)
    {
        if (id == CardId)
        {

        }
        Query.Where(e => e.CartId == CardId);
    }

    public ItemsInCartProjectionSpec(Guid id, Guid CardId,Guid ItemId)
    {
        if (id == CardId)
        {

        }
        Query.Where(e => e.CartId == CardId && e.ItemId == ItemId);
    }
}
