namespace LooseCouplingDemo.Domain;

/// <summary>
/// "Whenever a set of personalia is added or removed, we must store the updated number of persons."
/// </summary>
public class TrackPersonCountPolicy(
	IModifyPersonCountCommandHandler modifyPersonCountCommandHandler)
	: IPersonaliaChangedEventHandler // If this...
{
	public Task HandleAsync(PersonaliaChangedEvent @event, CancellationToken cancellationToken)
	{
		return @event.ModificationAction switch // ...then that
		{
			"Create" => modifyPersonCountCommandHandler.HandleAsync(+1, cancellationToken),
			"Delete" => modifyPersonCountCommandHandler.HandleAsync(-1, cancellationToken),
			_ => Task.CompletedTask,
		};

		// Loose coupling: we only know about the command, not WHO handles it or HOW
		// Command implies more EXPECTATION than event: we know WHAT and WHY, we require precisely one handler, and we disregard only the WHO and HOW
	}

	// I am a domain concept that is easy to test :)
}
