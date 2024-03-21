using Microsoft.AspNetCore.Mvc;
using SegundaPracticaMvcCore.Models;
using SegundaPracticaMvcCore.Repositories;

namespace SegundaPracticaMvcCore.ViewComponents
{
	public class MenuGenerosViewComponent: ViewComponent
	{
		private RepositoryLibros repo;
		public MenuGenerosViewComponent(RepositoryLibros repo)
		{
			this.repo = repo;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			List<Genero> generos = await this.repo.GetLGenerosAsync();
			return View(generos);
		}
	}
}
