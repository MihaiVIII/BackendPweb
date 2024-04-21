using System.Net;
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

public class AddressFservice : IAddressFService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public AddressFservice(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<AddressDTO>> GetAddress(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new AddrFProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ? 
            ServiceResponse<AddressDTO>.ForSuccess(result) : 
            ServiceResponse<AddressDTO>.FromError(CommonErrors.AddrNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<AddressDTO>>> GetAddreses(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new AddrFProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<AddressDTO>>.ForSuccess(result);
    }

    
    public async Task<ServiceResponse> AddAddr(AddressAddDTO address, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Client) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the client can add Addreses to self!", ErrorCodes.CannotAdd));
        }
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the client can add Addreses to self!", ErrorCodes.CannotAdd));
        }
        
        await _repository.AddAsync(new AdreseFacturare
        {
            City = address.City,
            Street = address.City,
            SNumber = address.SNumber,
            Scara = address.Scara,
            Bloc = address.Bloc,
            Apartament = address.Apartament,
            Description = address.Description,

            UserId = requestingUser.Id
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateAddr(AddressUpdateDTO addr, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Client && requestingUser.Id != addr.UserID) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Client update the address!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new AddrFSpec(addr.Id), cancellationToken); 

        if (entity != null) // Verify if the user is not found, you cannot update an non-existing entity.
        {
            entity.City = addr.City ?? entity.City;
            entity.Street = addr.Street ?? entity.Street;
            entity.SNumber = addr.SNumber ?? entity.SNumber;
            entity.Scara = addr.Scara ?? entity.Scara;
            entity.Bloc = addr.Bloc ?? entity.Bloc;
            entity.Apartament = addr.Apartament ?? entity.Apartament;
            entity.Description = addr.Description ?? entity.Description;
            
            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteAddr(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Client) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Client can delete the address!", ErrorCodes.CannotDelete));
        }
        var entity = await _repository.GetAsync(new AddrFSpec(id), cancellationToken);
        if (entity !=null && entity.UserId != requestingUser.Id)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Client can delete the address!", ErrorCodes.CannotDelete));
        }
        await _repository.DeleteAsync<AdreseFacturare>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<AddressDTO>> GetAddressFromUser(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new AddrFProjectionSpec(id,id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ?
            ServiceResponse<AddressDTO>.ForSuccess(result) :
            ServiceResponse<AddressDTO>.FromError(CommonErrors.AddrNotFound); // Pack the result or error into a ServiceResponse.
    }
}
