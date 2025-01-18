var builder = WebApplication.CreateBuilder(args);

// add services to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP Request Pipeline
app.MapCarter();

app.Run();
