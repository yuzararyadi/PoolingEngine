using Microsoft.EntityFrameworkCore;
using PoolingEngine;
using PoolingEngine.Data;
using PoolingEngine.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=PoolingQueue.db"));
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcReflectionService();
app.MapGrpcService<GreeterService>();
app.MapGrpcService<PoolingTagService>();
app.MapGrpcService<ManageDeviceItemService>();
app.MapGrpcService<ManageTagItemService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
