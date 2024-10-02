namespace LooseCouplingDemo.Domain;

/// <summary>
/// "Whenever a set of personalia has changed, we must announce that fact."
/// </summary>
public class AnnouncePersonaliaChangesPolicy
	: IPersonaliaChangedEventHandler // If this...
{
	public Task HandleAsync(PersonaliaChangedEvent @event, CancellationToken cancellationToken) // ...then that
	{
		Console.WriteLine($"Announcing personalia change on person {@event.PersonId}.");
		return Task.CompletedTask;
	}
}
