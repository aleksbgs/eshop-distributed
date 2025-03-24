namespace Basket.ApiClients;

public class CatalogApiClient(HttpClient client)
{
    public async Task<Product> GetProductById(int id)
    {
        var response = await client.GetFromJsonAsync<Product>($"/products/{id}");
        return response!;
    }
    
}