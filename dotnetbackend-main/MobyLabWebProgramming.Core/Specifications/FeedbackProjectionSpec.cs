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
public sealed class FeedbackProjectionSpec : BaseSpec<FeedbackProjectionSpec, Feedback, FeedbackDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<Feedback, FeedbackDTO>> Spec => e => new()
    {
        Score = e.Score,
        Anonimous = e.Anonimous,
        Quality = e.Quality,
        Content = e.Content,
        UserId = e.UserId,
        Date = e.CreatedAt
    };

    public FeedbackProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public FeedbackProjectionSpec(Guid id) : base(id)
    {
    }
}
