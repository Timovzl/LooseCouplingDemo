using LooseCouplingDemo.Domain;

namespace LooseCouplingDemo.Infrastructure;

public class DummySyncEntityCommandHandler : IModifyPersonCountCommandHandler
{
	public int Count { get; private set; }

	public Task HandleAsync(int deltaCount, CancellationToken cancellationToken)
	{
		this.Count += deltaCount;

		// Call external API or repository here :)
		Console.WriteLine($"Pretending to call some API or repository to store count {this.Count}.");
		return Task.CompletedTask;
	}

	// I am an infrastructural implementation detail
}
