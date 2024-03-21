using Microsoft.EntityFrameworkCore;
using SegundaPracticaMvcCore.Data;
using SegundaPracticaMvcCore.Models;
using System.Runtime.InteropServices;

namespace SegundaPracticaMvcCore.Repositories
{
    public class RepositoryLibros
    {
        private LibroContext context;
        public RepositoryLibros(LibroContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            Usuario usu = await this.context.Usuarios.Where(x => x.Email == email && x.Pass == password).FirstOrDefaultAsync();
            return usu;
        }

        public async Task<List<Libro>> GetLibrosAsync()
        {
            return await this.context.Libros.ToListAsync();
        }

        public async Task<Libro> FindLibroAsync(int idLibro)
        {
            return await this.context.Libros.FirstOrDefaultAsync(x => x.IdLibro == idLibro);
        }

        public async Task<List<Libro>> GetLibrosByGeneroAsync(int idGenero)
        {
            return await this.context.Libros.Where(x => x.IdGenero == idGenero).ToListAsync();
        }

        public async Task<List<Genero>> GetLGenerosAsync()
        {
            return await this.context.Generos.ToListAsync();
        }

        public async Task<List<Libro>> GetLibrosCarrito(List<int> idsLibros)
        {
            var consulta = from datos in this.context.Libros
                           where idsLibros.Contains(datos.IdLibro)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
        }

        public async Task<int> GetMaxIdPedidoAsync()
        {
            return await this.context.Pedidos.MaxAsync(x => x.IdPedido) + 1;
        }

        public async Task<int> GetMaxIdFacturaAsync()
        {
            return await this.context.Pedidos.MaxAsync(x => x.IdFactura) + 1;
        }

       public async Task CreateCompra(int idLibro, int id)
        {

        }
    }
}
