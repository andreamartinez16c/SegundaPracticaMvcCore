using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SegundaPracticaMvcCore.Filters
{
	public class AuthorizeUsersAttribute: AuthorizeAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			//EL USUARIO ESTARA DENTRO DE HttpContext Y SU PROPIEDAD USER
			//ESE USUARIO PERTENECE A LA CLASE PRINCIPAL E IDENTITY
			//MEDIANTE LA IDENTIDAD, PODEMOS SABER EL NOMBRE DEL USER
			//MEDIANTE EL PRINCIPAL, PODEMOS SABER EL ROLE DEL USER
			var user = context.HttpContext.User;
			//PREGUNTAMOS SI EL USER YA ESTA AUTENTIFICADO
			if (user.Identity.IsAuthenticated == false)
			{
				//CREAMOS LA RUTA A NUESTRA DIRECCION
				RouteValueDictionary rutaLogin =
					new RouteValueDictionary
					(
						new { controller = "Managed", action = "Login" }
					);
				//LLEVAMOS AL USUARIO A Login
				context.Result =
					new RedirectToRouteResult(rutaLogin);
			}

		}
	}
}
