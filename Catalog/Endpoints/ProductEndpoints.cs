namespace Catalog.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products");

        //GET all
        group.MapGet("/", async (ProductService service) =>
            {
                var products = await service.GetAllProductsAsync();
                return Results.Ok(products);
            })
            .WithName("GetAllProducts")
            .Produces<List<Product>>(StatusCodes.Status200OK);

        //GET by ID
        group.MapGet("/{id}", async (ProductService service, int id) =>
            {
                var product = await service.GetProductByIdAsync(id);
                if (product is null) return Results.NotFound();
                return Results.Ok(product);

            })
            .WithName("GetProductById")
            .Produces<Product>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        // POST (Create)
        group.MapPost("/", async (ProductService service, Product product) =>
            {
                await service.CreateProductAsync(product);
                return Results.Created($"/products/{product.Id}", product);

            })
            .WithName("CreateProduct")
            .Produces<Product>(StatusCodes.Status201Created);

        //PUT (Update)
        group.MapPut("/{id}", async (int id, Product inputProduct, ProductService service) =>
            {
                var updatedProduct = await service.GetProductByIdAsync(id);
                if (updatedProduct is null) return Results.NotFound();

                await service.UpdateProductAsync(updatedProduct, inputProduct);
                return Results.NoContent();

            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);
        
        
        //DELETE 
        group.MapDelete("/{id}", async (int id, ProductService service) =>
            {
                var deletedProduct = await service.GetProductByIdAsync(id);
                if (deletedProduct is null) return Results.NotFound();

                await service.DeleteProductAsync(deletedProduct);
                return Results.NoContent();

            })
            .WithName("DeletedProduct")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);
        
        //Support AI
        group.MapGet("/support/{query}", async (string query, ProductAiService service) =>
            {
                var response = await service.SupportAsync(query);
                return Results.Ok(response);
            })
            .WithName("GetSupport")
            .Produces(StatusCodes.Status200OK);


    }

}