using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoolingEngine.API.Helper;
using PoolingEngine.API.Hub;
using PoolingEngine.API.Worker;
using PoolingEngine.DataAccess.Context;
using PoolingEngine.DataAccess.Implementation;
using PoolingEngine.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure <HostOptions>(x =>
{
    x.ServicesStartConcurrently = true;
    x.ServicesStopConcurrently = false;
});
// Add services to the container.

builder.Services.AddHostedService<Worker>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DataSeeder>();
//Add Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
);
//builder.Services.AddDbContext<InMemoryDbContext>(options =>
//    options.UseInMemoryDatabase(databaseName: "RequestPooling"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddSignalR();

//builder.Services.AddScoped<IPoolingExecution,PoolingExecution>();
builder.Services.AddSingleton<IRequestItemRepository,RequestItemRepository>();
builder.Services.AddSingleton<IOpcUaRepository,OpcUaRepository>();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<PoolingBroadcastHub>("/PoolingBroadcast");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
