using System.Net;
using Ardalis.Specification;
using MailKit;
using Microsoft.AspNetCore.Http.Features;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class FeedbackSerivce : IFeedbackService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>

    public FeedbackSerivce(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetForms(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new FeedbackProjectionSpec(true), cancellationToken);

        return ServiceResponse<PagedResponse<FeedbackDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> SendFeedback(FeedbackDTO feedback, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var aux = await _repository.AddAsync(new Feedback
        {
           Score = feedback.Score,
           Anonimous = feedback.Anonimous,
           Quality = feedback.Quality,
           Content = feedback.Content
        }, cancellationToken); // A new entity is created and persisted in the database.
        if (requestingUser != null && !feedback.Anonimous)
        {
            aux.UserId = requestingUser.Id;
            await _repository.UpdateAsync(aux, cancellationToken);
        }
       
        return ServiceResponse.ForSuccess();
    }

}
