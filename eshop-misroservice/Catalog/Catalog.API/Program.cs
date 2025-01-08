var builder = WebApplication.CreateBuilder(args);

// add services to container

var app = builder.Build();

// Configure the HTTP Request Pipeline

app.Run();
