using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"));
});
builder.Services.AddCors();
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddJWTService(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure CORS
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the application
app.Run();

// Setup the database
SetupDatabase(app);

void SetupDatabase(IHost app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var dataContext = services.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
        Seed.SeedUsers(dataContext).Wait();
    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred during migration");
    }
}

