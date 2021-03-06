using Sales.Application;
using Sales.Infrastructure;
using Sales.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ }
app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

// Initialize and Seed database
await app.MigrateDatabaseAsync();

app.Run();
