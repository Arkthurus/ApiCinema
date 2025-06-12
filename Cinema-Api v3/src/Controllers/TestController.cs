using Microsoft.AspNetCore.Mvc;

namespace Cinema_Api.src.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
	[HttpGet]
	public string Teste()
	{
		return "Funciona!";
	}
}
