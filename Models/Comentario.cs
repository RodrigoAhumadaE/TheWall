#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class Comentario{
    [Key]
    public int ComentarioId  {get;set;}

    [Required(ErrorMessage = "Debe ingresar un comentario.")]
    public string ComentarioText {get;set;}

    public DateTime FechaCreacion {get;set;} = DateTime.Now;
    public DateTime FechaActualizacion {get;set;} = DateTime.Now;

    public int MensajeId {get;set;}

    public int UsuarioId {get;set;}

    public Mensaje? Mensaje {get;set;}

    public Usuario? Usuario {get;set;}
}