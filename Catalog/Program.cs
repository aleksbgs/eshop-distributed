var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


//Add services to container
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName:"catalogdb");
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductAIService>();
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

builder.AddOllamaSharpChatClient("ollama-llama3-2");

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
