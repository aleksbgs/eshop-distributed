



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


//Add services to container
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName:"catalogdb");
builder.Services.AddScoped<ProductService>();
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseMigration();
    app.MapOpenApi();
}


app.MapProductEndpoints();

app.UseHttpsRedirection();

app.Run();
