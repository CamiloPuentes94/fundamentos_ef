using projectef;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using projectef.models;

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

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria).Where(p => p.PrioridadTarea == projectef.models.Prioridad.Baja));
});

app.MapGet("/api/tareas2", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});

app.MapPost("/api/tareas2", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await dbContext.AddAsync(tarea); //De esta manera se puede agregar un registro

    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.Run();
