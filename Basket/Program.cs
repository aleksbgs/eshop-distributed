var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddServiceDefaults();
builder.AddRedisDistributedCache(connectionName:"cache");
builder.Services.AddScoped<BasketService>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapDefaultEndpoints();
app.MapBasketEndpoints();

app.UseHttpsRedirection();


app.Run();

