using System.ComponentModel.DataAnnotations;

namespace projectef.models;

public class Categoria
{
    //[Key] // DATA NOTION O ATRIBUTOS
    public Guid CategoriaId {get;set;}

    // [Required]
    // [MaxLength(150)]
    public string Nombre {get;set;}

    public string Descripcion {get;set;}

    public virtual ICollection<Tarea> Tareas {get;set;} 

}