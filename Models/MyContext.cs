#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace TheWall.Models;

public class MyContext : DbContext{
    public DbSet<Usuario> Usuarios {get;set;}
    public DbSet<Mensaje> Mensajes {get;set;}
    public DbSet<Comentario> Comentarios {get;set;}

    public MyContext(DbContextOptions options) : base(options){}
}