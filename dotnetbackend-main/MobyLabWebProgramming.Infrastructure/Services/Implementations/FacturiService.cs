using System.Net;
using Ardalis.Specification;
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

public class FacturiService : IFacturiService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public FacturiService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<FacturiDTO>> GetFacturi(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new FacturaProjectionSpec(id), cancellationToken); 

        return result != null ? 
            ServiceResponse<FacturiDTO>.ForSuccess(result) : 
            ServiceResponse<FacturiDTO>.FromError(CommonErrors.CartNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<FacturiDTO>>> GetUserFacturi(Guid id, PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new FacturaProjectionSpec(id,id), cancellationToken);

        return ServiceResponse<PagedResponse<FacturiDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> CreateFactura(Guid cartID,Guid AddrId, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Client) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the client can invoice!", ErrorCodes.CannotAdd));
        }
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the client can create invoice!", ErrorCodes.CannotAdd));
        }

        var cart = _repository.GetAsync(new SCartSpec(cartID),cancellationToken).Result;
        if (cart == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The Cart is empty!", ErrorCodes.CannotAdd));
        }
        var addr = _repository.GetAsync(new AddrFSpec(AddrId), cancellationToken).Result;
        if (addr == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "No address selected!", ErrorCodes.CannotAdd));
        }
        await _repository.AddAsync(new Facturi
        {
            Pret = cart.Price,
            CartId = cartID,
            IdAdd = AddrId,
            UserId = requestingUser.Id
        }, cancellationToken); // A new entity is created and persisted in the database.
        cart.InUse = false;
        await _repository.UpdateAsync(cart, cancellationToken);
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> CancelOrder(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if(requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Client can cancel orders!", ErrorCodes.CannotDelete));

        }
        if (requestingUser.Role != UserRoleEnum.Client) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Client can cancel orders!", ErrorCodes.CannotDelete));
        }

        var result = await _repository.GetAsync(new FacturiSpec(id), cancellationToken);
        if (result != null)
        {
            result.State = FacturiStateEnum.Cancelled;
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> ChagesOrderState(FacturiUpdateDTO upd, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can change orders!", ErrorCodes.CannotAdd));
        }
        if (requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can update orders!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new FacturiSpec(upd.Id), cancellationToken);

        if (entity != null) // Verify if the user is not found, you cannot update an non-existing entity.
        {
            entity.State = upd.State ?? entity.State;
            entity.ShippingTime = upd.ShippingTime ?? entity.ShippingTime;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

}
