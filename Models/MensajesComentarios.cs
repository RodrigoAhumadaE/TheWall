#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class MensajesComentarios{
    public List<Mensaje> mensajes {get;set;} = new List<Mensaje>();
    public Mensaje mensaje {get;set;}
    public Comentario comentario {get;set;}
}