using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IFeedbackService
{
    /// <summary>
    /// get all feedback
    /// </summary>
    public Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetForms(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// Creates Feedback
    /// </summary>
    public Task<ServiceResponse> SendFeedback(FeedbackDTO feedback,UserDTO requestingUser, CancellationToken cancellationToken = default);


}
