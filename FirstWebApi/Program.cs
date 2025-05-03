using FirstWebApi.Models;
using FirstWebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwaggerThemes;

var builder = WebApplication.CreateBuilder(args);

// Switch to GithubFirstWebApiNotes for GitHub testing, DefaultConnection for Mac and LocalWindowsConnection for Windows
var connectionString = builder.Configuration.GetConnectionString("GithubFirstWebApiNotes"); 


builder.Services.AddControllers();

// builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase(databaseName: "NotesList")); // When you dont want to setup database use this .net in memory functionality
builder.Services.AddDbContext<DataContextEF>(options => options.UseSqlServer(connectionString)); // Use the connection string name from appsettings.json
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("FirstWebApi"))
);

// builder.Services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("AppDb"));
builder
    .Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddApiEndpoints();
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

Console.WriteLine(connectionString);
Console.WriteLine("Environment : " + builder.Environment.EnvironmentName);

builder.Services.AddScoped<NotesService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
app.MapIdentityApi<IdentityUser>();

app.Run();

public partial class Program { }
