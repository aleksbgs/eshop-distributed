using MassTransit;


namespace Catalog.Services;

public class ProductService(ProductDbContext dbContext,IBus bus)
{

     public async Task<IEnumerable<Product>> GetAllProductsAsync()
     {
          return await dbContext.Products.ToListAsync();
     }

     public async Task<Product?> GetProductByIdAsync(int productId)
     {
          return await dbContext.Products.FindAsync(productId);
     }
     
     
     public async Task CreateProductAsync(Product product)
     {
          dbContext.Products.Add(product);
          await dbContext.SaveChangesAsync();
     }
     //TODO: Dual Write Distribution problem solved with saga pattern when we want to write something in two distributed systems transaction issue
     public async Task UpdateProductAsync(Product updatedProduct, Product inputProduct)
     {

          if (updatedProduct.Price != inputProduct.Price)
          {
               //publish event
               var integrationEvent = new ProductPriceChangedIntegrationEvent
               {
                    ProductId = updatedProduct.Id,
                    Name = inputProduct.Name,
                    Price = inputProduct.Price,
                    ImageUrl = inputProduct.ImageUrl,
                    Description = inputProduct.Description
               };
               await bus.Publish(integrationEvent);
          }
          
          updatedProduct.Name = inputProduct.Name;
          updatedProduct.Description = inputProduct.Description;
          updatedProduct.Price = inputProduct.Price;
          updatedProduct.ImageUrl = inputProduct.ImageUrl;
          dbContext.Products.Update(updatedProduct);
          await dbContext.SaveChangesAsync();
     }

     public async Task DeleteProductAsync(Product deletedProduct)
     {
          dbContext.Products.Remove(deletedProduct);
          await dbContext.SaveChangesAsync();
     }

     public async Task<IEnumerable<Product>> SearchProductAsync(string searchTerm)
     {
          return await dbContext.Products.Where(p => p.Name.Contains(searchTerm)).ToListAsync();
     }
     
}