using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;

namespace AdWindows.POC.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{
		[HttpGet("checkIfExists/{user}")]
		public string CheckIfExists(string? user)
		{
			if (!OperatingSystem.IsWindows())
				throw new Exception("Este m�todo funciona apenas com esta API hospedada no ambiente Windows.");

			try
			{
				using PrincipalContext context = new(ContextType.Domain);
				using PrincipalSearcher searcher = new(new UserPrincipal(context));

				bool exists = searcher.FindAll().Any(u => OperatingSystem.IsWindows() && u.SamAccountName == user);

				return exists ? "Existe!" : "N�o existe!";
			}
			catch (Exception ex)
			{
				throw new Exception("N�o foi poss�vel ler os usu�rios do dominio atual.", ex);
			}
		}
	}
}
