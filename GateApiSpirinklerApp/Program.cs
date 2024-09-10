using DataBaseService;
using DataBaseService.Infrastructure;
using GateApiSpirinklerApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Configuration
//    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("secrets.json");

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionstring == null)
    throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<SprinklerAppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("GateApiSpirinklerApp")));
//builder.Services.AddDbContext<SprinklerAppDbContext>();

builder.Services.AddScoped<UnitOfWork, UnitOfWork>(provider =>
{
    var dbContext = provider.GetRequiredService<SprinklerAppDbContext>();
    return new UnitOfWork(dbContext);
});

builder.Services.AddHttpClient();
builder.Services.AddHostedService<WeatherCheckService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
