using Microsoft.AspNetCore.Mvc;
using SegundaPracticaMvcCore.Extensions;
using SegundaPracticaMvcCore.Filters;
using SegundaPracticaMvcCore.Models;
using SegundaPracticaMvcCore.Repositories;

namespace SegundaPracticaMvcCore.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;
        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Libro> libros = await this.repo.GetLibrosAsync();
            return View(libros);
        }

        public async Task<IActionResult> DetallesLibro(int idlibro)
        {
            Libro libro = await this.repo.FindLibroAsync(idlibro);
            return View(libro);
        }

        public async Task<IActionResult> LibrosByGenero(int idgenero)
        {
            List<Libro> libros = await this.repo.GetLibrosByGeneroAsync(idgenero);
            return View(libros);
        }

        public async Task<IActionResult> SessionCarrito(int idLibro)
        {
            List<int> carritoLibros = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
            if (carritoLibros != null)
            {
                carritoLibros.Add(idLibro);
            }
            else
            {
                carritoLibros = new List<int>();
                carritoLibros.Add(idLibro);
            }
            HttpContext.Session.SetObject("IDSLIBROS", carritoLibros);
            return RedirectToAction("DetallesLibro", new { idLibro = idLibro });
        }

        public async Task<IActionResult> Carrito(int? ideliminar)
        {
            List<int> carritoLibros = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
            if (carritoLibros != null)
            {
                if (ideliminar != null)
                {
                    carritoLibros.Remove(ideliminar.Value);
                    if (carritoLibros.Count == 0)
                    {
                        HttpContext.Session.Remove("IDSLIBROS");
                    }
                    else
                    {
                        HttpContext.Session.SetObject("IDSLIBROS", carritoLibros);
                    }
                }
                List<Libro> libros = await this.repo.GetLibrosCarrito(carritoLibros);
                return View(libros);
            }
            return View();
        }

        [AuthorizeUsers]
        public async Task<IActionResult> Comprar()
        {
            List<int> carritoLibros = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
            List<Libro> libros = await this.repo.GetLibrosCarrito(carritoLibros);
            int idUser = int.Parse(HttpContext.User.FindFirst("ID").Value);
            int idFactura = await this.repo.GetMaxIdFacturaAsync();
            foreach (Libro libro in libros)
            {
                await this.repo.CreateCompra(libro.IdLibro, idFactura, idUser, 1);
            }
            HttpContext.Session.Remove("IDSLIBROS");
            return RedirectToAction("Pedidos");
        }

        [AuthorizeUsers]
        public async Task<IActionResult> Pedidos()
        {
            List<VistaPedido> pedidos = await this.repo.GetVistaPedidosAsync();
            return View(pedidos);
        }
    }
}
