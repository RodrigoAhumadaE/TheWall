using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TheWall.Models;
namespace TheWall.Controllers;

public class MensajeController : Controller{
    private readonly ILogger<MensajeController> _logger;
    private MyContext _context;
    
    public MensajeController(ILogger<MensajeController> logger, MyContext context){
        _logger = logger;
        _context = context;
    }

    // GET
    [HttpGet("muro")]
    [SessionCheck]
    public IActionResult Muro(){
        string? email = HttpContext.Session.GetString("email");
        Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
        HttpContext.Session.SetInt32("id", (Int32)usuario.UsuarioId);
        HttpContext.Session.SetString("nombreUsuario", $"{usuario.Nombre} {usuario.Apellido}");
        // List<Mensaje> listaMensajes = _context.Mensajes.Include(m => m.Creador).Include(m => m.Comentarios).ThenInclude(m => m.Usuario).ToList();
        MensajesComentarios mensCom = new MensajesComentarios(){
            mensajes = _context.Mensajes.Include(m => m.Creador).Include(m => m.Comentarios).ThenInclude(m => m.Usuario).OrderByDescending(m => m.MensajeId).ToList(),
        };
        return View("Muro", mensCom);
    }

    // POST
    [HttpPost("procesa/mensaje")]
    public IActionResult ProcesaMensaje(Mensaje mensaje){
        if(ModelState.IsValid){
            Console.WriteLine("todo correcto");
            _context.Mensajes.Add(mensaje);
            _context.SaveChanges();
            RedirectToAction("Muro");
        }
        MensajesComentarios mensCom = new MensajesComentarios(){
            mensajes = _context.Mensajes.Include(m => m.Creador).Include(m => m.Comentarios).ThenInclude(m => m.Usuario).OrderByDescending(m => m.MensajeId).ToList(),
        };
        return View("Muro", mensCom);
    }

    [HttpPost("procesa/comentario")]
    public IActionResult ProcesaComentario(Comentario comentario){
        if(ModelState.IsValid){
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();
            RedirectToAction("Muro");
        }
        MensajesComentarios mensCom = new MensajesComentarios(){
            mensajes = _context.Mensajes.Include(m => m.Creador).Include(m => m.Comentarios).ThenInclude(m => m.Usuario).OrderByDescending(m => m.MensajeId).ToList(),
        };
        return View("Muro", mensCom);
    }
}

public class SessionCheckAttribute : ActionFilterAttribute{
    public override void OnActionExecuting(ActionExecutingContext context){
        string? email = context.HttpContext.Session.GetString("email");
        if(email == null){
            context.Result = new RedirectToActionResult("Index", "Usuario", null);
        }
    }
}