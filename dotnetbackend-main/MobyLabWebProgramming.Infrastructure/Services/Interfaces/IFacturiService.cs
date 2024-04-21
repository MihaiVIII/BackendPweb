using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IFacturiService
{
    /// <summary>
    /// get entity by its id
    /// </summary>
    public Task<ServiceResponse<FacturiDTO>> GetFacturi(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// get entity list from their users id
    /// </summary>
    public Task<ServiceResponse<PagedResponse<FacturiDTO>>> GetUserFacturi(Guid id, PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// Creates factura from cart
    /// </summary>
    public Task<ServiceResponse> CreateFactura(Guid cartID,Guid AddrId,UserDTO requestingUser, CancellationToken cancellationToken = default);
    /// <summary>
    /// cancels order
    /// </summary>
    public Task<ServiceResponse> CancelOrder(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    /// <summary>
    /// cancels order
    /// </summary>
    public Task<ServiceResponse> ChagesOrderState(FacturiUpdateDTO upd, UserDTO requestingUser, CancellationToken cancellationToken = default);

}
