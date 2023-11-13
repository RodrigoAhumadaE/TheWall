#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class Mensaje{

    [Key]
    public int MensajeId  {get;set;}
    
    [Required(ErrorMessage = "Debe ingresar Mensaje.")]
    public string MensajeTexto {get;set;}

    public DateTime FechaCreacion {get;set;} = DateTime.Now;
    public DateTime FechaActualizacion {get;set;} = DateTime.Now;

    public int UsuarioId  {get;set;}

    public Usuario? Creador {get;set;}

    public List<Comentario> Comentarios {get;set;} = new List<Comentario>();
}