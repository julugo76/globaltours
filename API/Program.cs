using API.Helpers;
using Core.Interfaces;
using Infrastructure.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DATABASE
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
     Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql")));
// ServerVersion.AutoDetect(connectionString)));


//SERVICES
builder.Services.AddScoped<ILugarRepositorio, LugarRepositorio>();
builder.Services.AddScoped(typeof(IRepositorio<>),(typeof(Repositorio<>)));

builder.Services.AddAutoMapper(typeof(MappingProfiles));


var app = builder.Build();

//Configurar deteccion de cambios en migrations al ejecutar la app y alimentar la BD
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        await BaseDatosSeed.SeedAsync(context, loggerFactory);


    }
    catch (System.Exception ex)
    {

        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "un error ocurrio durante la migración");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
