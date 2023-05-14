using EFCoreRelationshipsSQLServer;
using EFCoreRelationshipsSQLServer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//user and character it's 1:n relationship
//character and weapon is 1:1 relation
//character and skills is n:n relation

// Add services to the container.

//add db connection?

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<Character>(); // razlicnie varianti zizni objecta
builder.Services.AddTransient<User>(); // 
builder.Services.AddSingleton<User>(); // vsegda zivet

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
