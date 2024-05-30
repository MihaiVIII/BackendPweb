using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ISCartService
{
    /// <summary>
    ///  GetCart will provide the information about an Cart given its Id.
    /// </summary>
    public Task<ServiceResponse<CartDTO>> GetCart(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    ///  GetCart will provide the information about an Cart given its users Id.
    /// </summary>
    public Task<ServiceResponse<CartDTO>> GetUserCart(UserDTO requestingUser, CancellationToken cancellationToken = default);

    /// <summary>
    /// returns all items from a cart from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<ItemDTO>>> GetItems(Guid id,PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a cart for specified user if there is not one already in use. User must be a client
    /// </summary>
    public Task<ServiceResponse> CreateCart(UserDTO requestingUser, CancellationToken cancellationToken = default);
    /// <summary>
    /// Adds item to specified cart from the id. If the item is not already in cart it adds it in cart
    /// If item already in cart it adds the quantity in of the products
    /// </summary>
    public Task<ServiceResponse> AddItemToCart(Guid id,ItemDTO item, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// Deletes the item from the cart. 
    /// If the item quantity is not equall to the item in cart quantity, the quantity is simply deducted
    /// </summary>
    public Task<ServiceResponse> RemoveItemFromCart(Guid id, ItemDTO item, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    ///<summary>
    /// Deletes in use cart
    /// </summary>
    public Task<ServiceResponse> DeleteCart(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);

   
}
