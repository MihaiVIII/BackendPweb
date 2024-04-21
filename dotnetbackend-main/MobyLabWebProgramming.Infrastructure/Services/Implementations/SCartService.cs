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

public class SCartService : ISCartService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public SCartService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<CartDTO>> GetCart(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new SCartProjectionSpec(id), cancellationToken); 

        return result != null ? 
            ServiceResponse<CartDTO>.ForSuccess(result) : 
            ServiceResponse<CartDTO>.FromError(CommonErrors.CartNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<CartDTO>> GetUserCart(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new SCartProjectionSpec(id,id), cancellationToken);

        return result != null ?
            ServiceResponse<CartDTO>.ForSuccess(result) :
            ServiceResponse<CartDTO>.FromError(CommonErrors.CartNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<ItemDTO>>> GetItems(Guid id, PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ItemsInCartProjectionSpec(id,id), cancellationToken);

        foreach (var item in result.Data)
        {
            var aux = await _repository.GetAsync(new ItemProjectionSpec(item.Id), cancellationToken);
            if (aux != null)
            {
                item.Name = aux.Name;
                item.Price = aux.Price;
            }
        }
        return ServiceResponse<PagedResponse<ItemDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> CreateCart(UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Client) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the client can create cart to self!", ErrorCodes.CannotAdd));
        }
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the client can create cart to self!", ErrorCodes.CannotAdd));
        }

        await _repository.AddAsync(new ShopCart
        {
            Price = 0,
            Count = 0,

            UserId = requestingUser.Id
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> AddItemToCart(Guid id, ItemDTO item, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {   
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Client) 
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the client can update the cart!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new SCartSpec(id), cancellationToken); 
        var prod = await _repository.GetAsync(new ItemsSpec(item.Id), cancellationToken);
        if (entity != null && prod != null && prod.Quantity > 0)
        {
            entity.Price += item.Price;
            entity.Count += 1;
            prod.Quantity -= 1;
            await _repository.UpdateAsync(entity, cancellationToken);
            await _repository.UpdateAsync(prod, cancellationToken);

            var prod_in_cart = await _repository.GetAsync(new ItemsInCartSpec(id, id,item.Id), cancellationToken);
            if (prod_in_cart != null)
            {
                prod_in_cart.Quantity += 1;
            }
            else
            {
                await _repository.AddAsync(new Item_In_Carts
                {
                    Quantity = 1,
                    CartId = id,
                    ItemId = item.Id
                    
                }, cancellationToken);
            }
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> RemoveItemFromCart(Guid id, ItemDTO item, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Client)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the client can update the cart!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new SCartSpec(id), cancellationToken);
        var prod = await _repository.GetAsync(new ItemsSpec(item.Id), cancellationToken);
        if (entity != null && prod != null && entity.Count > 0)
        {
            entity.Price -= item.Price;
            entity.Count -= 1;
            prod.Quantity += 1;
            await _repository.UpdateAsync(entity, cancellationToken);
            await _repository.UpdateAsync(prod, cancellationToken);

            var prod_in_cart = await _repository.GetAsync(new ItemsInCartSpec(id, id, item.Id), cancellationToken);
            if (prod_in_cart != null)
            {
                prod_in_cart.Quantity -= 1;
                if(prod_in_cart.Quantity <= 0)
                {
                    await _repository.DeleteAsync<Item_In_Carts>(prod_in_cart.Id, cancellationToken);
                }
            }
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCart(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if(requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Producer can delete the item!", ErrorCodes.CannotDelete));

        }
        if (requestingUser.Role == UserRoleEnum.Client) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Producer can delete the item!", ErrorCodes.CannotDelete));
        }
        if (requestingUser.Role != UserRoleEnum.Producer)
        {
            var entity = await _repository.GetAsync(new ItemsSpec(id), cancellationToken);
            if (entity !=null && entity.UserId != requestingUser.Id)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Producer can delete the item!", ErrorCodes.CannotDelete));
            }
        }
        
        await _repository.DeleteAsync<Items>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }


}
