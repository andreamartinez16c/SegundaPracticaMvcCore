using Microsoft.EntityFrameworkCore;
using SegundaPracticaMvcCore.Models;

namespace SegundaPracticaMvcCore.Data
{
    public class LibroContext: DbContext
    {
        public LibroContext(DbContextOptions<LibroContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VistaPedido> VistaPedidos { get; set; }
    }
}
