namespace Basket.Endpoints;

public static class BasketEndpoints
{
    public static void MapBasketEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("basket");

        group.MapGet("/{userName}", async (string userName, BasketService basketService) =>
            {
                var basket = await basketService.GetBasket(userName);
                if (basket is null) return Results.NotFound();
                return Results.Ok(basket);
            })
            .WithName("GetBasket")
            .Produces<ShoppingCart>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (ShoppingCart shoppingCart, BasketService basketService) =>
            {
                await basketService.UpdateBasket(shoppingCart);
                return Results.Ok();
            })
            .WithName("UpdateBasket")
            .Produces<ShoppingCart>(StatusCodes.Status204NoContent);

        group.MapDelete("/{userName}", async (string userName, BasketService basketService) =>
            {
                await basketService.DeleteBasket(userName);
                return Results.NoContent();
            })
            .WithName("DeleteBasket")
            .Produces(StatusCodes.Status204NoContent);



    }
}

