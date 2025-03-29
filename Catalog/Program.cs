using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


//Add services to container
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName:"catalogdb");
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductAiService>();
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

builder.AddOllamaSharpChatClient("ollama-llama3-2");
builder.AddOllamaSharpEmbeddingGenerator("ollama-all-minilm");

builder.Services.AddInMemoryVectorStoreRecordCollection<int,ProductVector>("products");

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
