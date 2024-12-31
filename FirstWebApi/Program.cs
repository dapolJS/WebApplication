using FirstWebApi.Services;
using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;
using SwaggerThemes;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing")
{ // ASPNETCORE_ENVIRONMENT needs to be set to Testing to use test DB
    connectionString = builder.Configuration.GetConnectionString("GithubFirstWebApiNotes");
}

// Add services to the container.
builder.Services.AddControllers();
// builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase(databaseName: "NotesList")); // When you dont want to setup database use this .net in memory functionality
// Use SQL Server for DataContextEF
builder.Services.AddDbContext<DataContextEF>(options => options.UseSqlServer(connectionString)); // Use the connection string name from appsettings.json
builder.Services.AddDbContext<TestDataContextEF>(options => options.UseSqlServer(connectionString)); //Add-Migration InitialCreate -Context TestDataContextEF; Update-Database -Context TestDataContextEF

Console.WriteLine(connectionString);
Console.WriteLine("Environment : " + builder.Environment.EnvironmentName);

// Register NotesService with Dependency Injection
builder.Services.AddScoped<NotesService>();
builder.Services.AddScoped<NotebooksService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(Theme.NordDark);
}
else if (app.Environment.IsEnvironment("Testing"))
{
    app.UseSwagger();
    app.UseSwaggerUI(Theme.NordDark);
}
else
{
    app.UseHttpsRedirection();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }