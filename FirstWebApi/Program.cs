using FirstWebApi.Services;
using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;
using SwaggerThemes;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GithubFirstWebApiNotes");
Console.WriteLine("=====================> " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

// Add services to the container.
builder.Services.AddControllers();
// builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase(databaseName: "NotesList")); // When you dont want to setup database use this .net in memory functionality
// Use SQL Server for DataContextEF
builder.Services.AddDbContext<DataContextEF>(options => options.UseSqlServer(connectionString)); // Use the connection string name from appsettings.json

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
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }