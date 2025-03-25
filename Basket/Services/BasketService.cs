using System.Text.Json;
using Basket.ApiClients;
using Microsoft.Extensions.Caching.Distributed;


namespace Basket.Services;

public class BasketService(IDistributedCache cache,CatalogApiClient catalogApiClient)
{
    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basket = await cache.GetStringAsync(userName);
        return string.IsNullOrEmpty(basket) ? null : JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task UpdateBasket(ShoppingCart basket)
    {
        foreach (var item in basket.Items)
        {
            var product = await catalogApiClient.GetProductById(item.ProductId);
            
            item.Price = product.Price;
            item.ProductName = product.Name;
        }

        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));
    }

    public async Task DeleteBasket(string userName)
    {
        await cache.RemoveAsync(userName);
    }

    internal async Task UpdateBasketItemProductPrices(int productId, decimal price)
    {
        //IDistributedCache not supported list of keys function 
        //https://github.com/dotnet/runtime/issues/36402
        
        //you need loop through all keys in redis and updated cache so we hard code here user name just for showing purpose

        var basket = await GetBasket("swn");

        var item = basket.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            item.Price = price;
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(item));
        }
        
    }
    
 

}