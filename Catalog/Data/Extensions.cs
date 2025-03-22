namespace Catalog.Data;

public static class Extensions
{
    public static void UseMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        context.Database.Migrate();
        
        DataSeeder.Seed(context);
    }
    
}

public class DataSeeder
{
    public static void Seed(ProductDbContext dbContext)
    {
        if (dbContext.Products.Any())
            return;
        
        dbContext.Products.AddRange(Products);
        dbContext.SaveChanges();
    }

    public static IEnumerable<Product> Products =>
    [
        new Product { Name = "solar", Description = "desc", Id = 1, ImageUrl = "url", Price = 3 },
        new Product { Name = "solar", Description = "desc", Id = 2, ImageUrl = "url", Price = 3 },
        new Product { Name = "solar", Description = "desc", Id = 3, ImageUrl = "url", Price = 3 },
        new Product { Name = "solar", Description = "desc", Id = 4, ImageUrl = "url", Price = 3 },
        new Product { Name = "solar", Description = "desc", Id = 5, ImageUrl = "url", Price = 3 },
        new Product { Name = "solar", Description = "desc", Id = 6, ImageUrl = "url", Price = 3 },
        new Product { Name = "solar", Description = "desc", Id = 7, ImageUrl = "url", Price = 3 },
        new Product { Name = "solar", Description = "desc", Id = 8, ImageUrl = "url", Price = 3 },
    ];

}