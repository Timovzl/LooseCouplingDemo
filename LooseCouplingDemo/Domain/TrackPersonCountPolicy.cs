namespace LooseCouplingDemo.Domain;

/// <summary>
/// "Whenever a set of personalia is added or removed, we must store the updated number of persons."
/// </summary>
public class TrackPersonCountPolicy
	: IPersonaliaChangedEventHandler // If this...
{
	private readonly IModifyPersonCountCommandHandler _modifyPersonCountCommandHandler;

	// Pro tip: Definitely go and apply the analyzer's suggestion of using a primary ctor (and removing fields) here!
	public TrackPersonCountPolicy(
		IModifyPersonCountCommandHandler modifyPersonCountCommandHandler)
	{
		this._modifyPersonCountCommandHandler = modifyPersonCountCommandHandler;
	}

	public Task HandleAsync(PersonaliaChangedEvent @event, CancellationToken cancellationToken) // ...then that
	{
		return @event.ModificationAction switch
		{
			"Create" => this._modifyPersonCountCommandHandler.HandleAsync(+1, cancellationToken),
			"Delete" => this._modifyPersonCountCommandHandler.HandleAsync(-1, cancellationToken),
			_ => Task.CompletedTask,
		};

		// Loose coupling: we only know about the command, not WHO handles it or HOW
		// Command implies more EXPECTATION than event: we know WHAT and WHY, we require precisely one handler, and we disregard only the WHO and HOW
	}

	// I am a domain concept that is easy to test :)
}
