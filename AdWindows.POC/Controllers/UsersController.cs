using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;

namespace AdWindows.POC.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{
		[HttpGet("{user}/{password}")]
		public string CheckIfExists(string? user, string? password)
		{
			if (!OperatingSystem.IsWindows())
				throw new Exception("Este método funciona apenas com esta API hospedada no ambiente Windows.");

			try
			{
				using PrincipalContext context = new(ContextType.Domain);

				bool isValid = context.ValidateCredentials(user, password);

				return isValid ? "Credenciais válidas!" : "Credenciais inválidas!";
			}
			catch (Exception ex)
			{
				throw new Exception("Não foi possível ler os usuários do dominio atual.", ex);
			}
		}
	}
}
