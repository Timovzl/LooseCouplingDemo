using LooseCouplingDemo.Application;
using LooseCouplingDemo.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LooseCouplingDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonaliaController : ControllerBase
{
	/// <summary>
	/// Allows third parties to send us push updates of changes to their entities.
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> PostPersonaliaChangeAsync(IngestPersonaliaChangeCommandHandler commandHandler, [FromBody] PostPushUpdateRequest request, CancellationToken cancellationToken)
	{
		if (request is null)
			return this.BadRequest("A request is required.");

		await commandHandler.HandleAsync(request, cancellationToken);

		return this.Ok();
	}
}
