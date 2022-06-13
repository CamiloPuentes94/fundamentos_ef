using Microsoft.EntityFrameworkCore;
using projectef.models;

namespace projectef;

public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    public TareasContext(DbContextOptions<TareasContext> options): base (options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() {
            CategoriaId = Guid.Parse("6067e125-9876-4da4-9c73-5011b00dd3cf"),
            Nombre = "Actividades pendientes",
            Peso = 20
            });

        categoriasInit.Add(new Categoria() {
            CategoriaId = Guid.Parse("6067e125-9876-4da4-9c73-5011b00dd330"),
            Nombre = "Actividades Personales",
            Peso = 40
            });


        modelBuilder.Entity<Categoria> (categoria =>{
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p => p.Descripcion);
            categoria.Property(p => p.Peso);

            categoria.HasData(categoriasInit)
        });

        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea() {
            TareaId = Guid.Parse("6067e125-9876-4da4-9c73-5011b00dd3ad"),
            CategoriaId = Guid.Parse("6067e125-9876-4da4-9c73-5011b00dd3cf"),
            PrioridadTarea = Prioridad.Media,
            Titulo = "Pago de servicios publicos",
            FechaCreacion = DateTime.Now
            });

        tareasInit.Add(new Tarea() {
            TareaId = Guid.Parse("6067e125-9876-4da4-9c73-5011b00dd312"),
            CategoriaId = Guid.Parse("6067e125-9876-4da4-9c73-5011b00dd330"),
            PrioridadTarea = Prioridad.Baja,
            Titulo = "Terminar de ver pelicula en netflix",
            FechaCreacion = DateTime.Now
            });

        modelBuilder.Entity<Tarea> (tarea  => {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);
            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId); //de este modo podemos configurar las llaves foraneas
            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(p => p.Descripcion);
            tarea.Property(p => p.PrioridadTarea);
            tarea.Property(p => p.FechaCreacion);
            tarea.Ignore(p => p.Resumen);

            tarea.HasData(tareasInit);

        });
    }
    
}