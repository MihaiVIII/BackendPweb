using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class SCartSpec : BaseSpec<SCartSpec, ShopCart>
{
    public SCartSpec(Guid id) : base(id)
    {
    }

    public SCartSpec(Guid UserId,bool state)
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