using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class ItemsInCartSpec : BaseSpec<ItemsInCartSpec, Item_In_Carts>
{
    public ItemsInCartSpec(Guid id) : base(id)
    {
    }

    public ItemsInCartSpec(Guid id, Guid CardId, Guid ItemId)
    {
        if (id == CardId)
        {

        }
        Query.Where(e => e.CartId == CardId && e.ItemId == ItemId);
    }
}