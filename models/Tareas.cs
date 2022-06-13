using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectef.models;

public class Tarea
{
    //[Key] // Attributos
    public Guid TareaId {get;set;}

    //[ForeignKey("CategoriaId")] // atributo llave foranea
    public Guid CategoriaId {get;set;}

    //[Required]
    //[MaxLength(200)]
    public string Titulo {get;set;}

    public string Descripcion {get;set;}

    public Prioridad PrioridadTarea {get;set;}

    public DateTime FechaCreacion {get;set;}

    public virtual Categoria Categoria {get;set;}

    //[NotMapped] // con este atributo no carga el mapeo para la base de datos
    public string Resumen {get;set;}
}

public enum Prioridad
{
    Baja,
    Media,
    alta
}