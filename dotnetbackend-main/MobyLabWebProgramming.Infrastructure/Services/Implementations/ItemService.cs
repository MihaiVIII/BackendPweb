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

public class ItemService : IItemService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public ItemService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ItemDTO>> GetItem(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ItemProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ? 
            ServiceResponse<ItemDTO>.ForSuccess(result) : 
            ServiceResponse<ItemDTO>.FromError(CommonErrors.ItemNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<ItemDTO>>> GetItems(PaginationSearchQueryParams pagination, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ItemProjectionSpec(pagination.Search,requestingUser), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<ItemDTO>>.ForSuccess(result);
    }

    
    public async Task<ServiceResponse> AddItem(ItemAddDTO item, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Producer) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the producer can add product to self!", ErrorCodes.CannotAdd));
        }
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the producer can add product to self!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new ItemsSpec(item.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The Item already exists!", ErrorCodes.UserAlreadyExists));
        }

        await _repository.AddAsync(new Items
        {
            Name = item.Name,
            Price = item.Price,
            Quantity = item.Quantity,

            UserId = requestingUser.Id
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateItem(ItemUpdateDTO item, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the producer can add product to self!", ErrorCodes.CannotAdd));
        }
        if (requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != item.Producer) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the producer update the products!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new ItemsSpec(item.Id), cancellationToken); 

        if (entity != null) // Verify if the user is not found, you cannot update an non-existing entity.
        {
            entity.Price = item.Price ?? entity.Price;
            entity.Name = item.Name ?? entity.Name;
            entity.Quantity = item.Quantity ?? entity.Quantity;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteItem(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Producer can delete the item!", ErrorCodes.CannotDelete));

        }
        if (requestingUser.Role == UserRoleEnum.Client) 
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Producer can delete the item!", ErrorCodes.CannotDelete));
        }
        if (requestingUser.Role == UserRoleEnum.Producer)
        {
            var entity = await _repository.GetAsync(new ItemsSpec(id), cancellationToken);
            if (entity != null && entity.UserId != requestingUser.Id)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Producer can delete the item!", ErrorCodes.CannotDelete));
            }
        }

        await _repository.DeleteAsync<Items>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<int>> GetItemCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<User>(cancellationToken)); // Get the count of all user entities in the database.

    public async Task<ServiceResponse> AddItemAdmin(ItemAdminAddDTO item, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {

        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add product!", ErrorCodes.CannotAdd));
        }
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the producer can add product!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new ItemsSpec(item.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The Item already exists!", ErrorCodes.UserAlreadyExists));
        }

        await _repository.AddAsync(new Items
        {
            Name = item.Name,
            Price = item.Price,
            Quantity = item.Quantity,

            UserId = item.UserId
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }
}
