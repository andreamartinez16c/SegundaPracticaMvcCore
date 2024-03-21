using Microsoft.AspNetCore.Mvc;
using SegundaPracticaMvcCore.Filters;
using SegundaPracticaMvcCore.Repositories;

namespace SegundaPracticaMvcCore.Controllers
{
	public class UsuariosController : Controller
	{
		[AuthorizeUsers]
		public IActionResult Perfil()
		{
			return View();
		}
	}
}
