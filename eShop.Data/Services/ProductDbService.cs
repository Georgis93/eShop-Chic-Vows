﻿
using eShop.API.DTO;
using eShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data.Services;

public class ProductDbService : DbService
{
    public ProductDbService(EShopContext db, IMapper mapper) : base(db, mapper)
    {

    }
    public async Task<List<ProductGetDTO>> GetProductsByCategoryAsync(int categoryId)
    {
        IncludeNavigationsFor<Color>();
        IncludeNavigationsFor<Size>();
        var productIds = GetAsync<ProductCategory>(pc => pc.CategoryId.Equals(categoryId))
            .Select(pc => pc.ProductId);
        var products = await GetAsync<Product>(p => productIds.Contains(p.Id)).ToListAsync();
        return MapList<Product, ProductGetDTO>(products);
    }
    public List<TDto> MapList<TEntity, TDto>(List<TEntity> entities)
    where TEntity : class
    where TDto : class
    {
        return _mapper.Map<List<TDto>>(entities);
    }
}
