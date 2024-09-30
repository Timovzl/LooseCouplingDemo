using LooseCouplingDemo.Common.Events;
using LooseCouplingDemo.Contracts;
using LooseCouplingDemo.Domain;

namespace LooseCouplingDemo.Application;

/// <summary>
/// "Whenever we ingest a personalia change update, we broadcast an event that describes in our own Bounded Context's terms what has happened (irrespective of who cares)."
/// </summary>
public class IngestPersonaliaChangeCommandHandler(
	IEventBus eventBus)
{
	public Task HandleAsync(PostPushUpdateRequest request, CancellationToken cancellationToken)
	{
		var @event = new PersonaliaChangedEvent()
		{
			EventId = Guid.NewGuid().ToString(),
			PersonId = request.Id,
			ModificationAction = request.ModificationAction,
		};

		return eventBus.PublishAsync(@event, cancellationToken);

		// Loose coupling: we only know about the event, not WHO handle it or HOW or WHY
	}
}
