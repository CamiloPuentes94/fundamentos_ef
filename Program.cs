using projectef;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB")); // base de datos en memoria

builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas")); // conexion a el servidor

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion",async([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Basede datos en memoria: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext)=>
{
    return Results.Ok(dbContext.Tareas)
});
app.Run();
