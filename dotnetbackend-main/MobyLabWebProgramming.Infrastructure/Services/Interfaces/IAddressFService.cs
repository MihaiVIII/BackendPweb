using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IAddressFService
{
    /// <summary>
    /// GetAddress will provide the information about an Address given its user Id.
    /// </summary>
    public Task<ServiceResponse<AddressDTO>> GetAddress(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetAddreses returns page with Address information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<AddressDTO>>> GetAddreses(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// AddAddr adds an addr and verifies if requesting user has permissions to add one.
    /// If the requesting addr is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddAddr(AddressAddDTO address, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateAddr updates an addr and verifies if requesting user has permissions to update
    /// If the requesting addr is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateAddr(AddressUpdateDTO addr, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteAddr deletes an addr and verifies if requesting user has permissions to delete it, if the addr is his own then that should be allowed.
    /// If the requesting addr is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteAddr(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<AddressDTO>> GetAddressFromUser(Guid id, CancellationToken cancellationToken = default);
}
