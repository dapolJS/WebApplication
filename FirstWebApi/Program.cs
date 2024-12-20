using FirstWebApi.Services;
using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;
using SwaggerThemes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase(databaseName: "NotesList"));

// Register NotesService with Dependency Injection
builder.Services.AddScoped<NotesService>();
builder.Services.AddScoped<NotebooksService>()

/* TODO: 
* 1. ONGOING >>> I need to create some special token(room) which will have notebooks sharable
* 2. DONE === If notebook exists do not allow to create new one
* 3. DONE === Each note should be easily assigned to notebook without new notebook being created
* 4. DONE === Each note should be easy to edit if not entering any values or leaving empty strings
* 5. DONE === Throw bad requests instead of new errors
* 6. Add AUTH based on token with expiration time ?
* 7. DONE === Fix note.Done issue that is note.Done not provided it autofills as false even if value existed
* 8. DONE === Try to init DTO's for notebook perhaps ? and others if possible
* 9. DONE === Separate Notebook controllers and Room controllers from Notes controlles
* 10. Improve apis to get many and get 1 with single endpoint
* 11. Improve delete endpoint to delete 1 and delete all notes
* 12. Test all endpoits thoroughly!
*/

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
