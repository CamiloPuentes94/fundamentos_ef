using projectef;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using projectef.models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB")); // base de datos en memoria

builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas")); // conexion a el servidor

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// hacer llamado de datos de la base de datos

app.MapGet("/dbconexion",async([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Basede datos en memoria: " + dbContext.Database.IsInMemory());
});

// hacer llamado de datos de la tabla tareas

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria).Where(p => p.PrioridadTarea == projectef.models.Prioridad.Baja));
});

app.MapGet("/api/tareas2", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});

// hacer llamado de datos de la tabla categoria

app.MapGet("/api/categoria", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Categorias);
});

//Agregar Registros

app.MapPost("/api/tareas2", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await dbContext.AddAsync(tarea); //De esta manera se puede agregar un registro

    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

// actualizar datos


app.MapPut("/api/tareas2/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{
    var tareaActual = dbContext.Tareas.Find(id); // variable para buscar por id

    if (tareaActual != null)
    {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;

        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapPut("/api/categoria/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Categoria categoria, [FromRoute] Guid id) =>
{
    var categoriaActual = dbContext.Categorias.Find(id);

    if (categoriaActual != null)
    {
        categoriaActual.Nombre = categoria.Nombre;
        categoriaActual.Descripcion = categoria.Descripcion;
        categoriaActual.Peso = categoria.Peso;
        

        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.Run();
