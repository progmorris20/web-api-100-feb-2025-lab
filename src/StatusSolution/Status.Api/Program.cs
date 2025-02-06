var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton(_ => TimeProvider.System);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapGet("/status",(TimeProvider clock) => {
    return new StatusResponseModel(clock.GetLocalNow(), "Looks Good");
    });

app.Run();


public record StatusResponseModel(DateTimeOffset Checked, string Message);