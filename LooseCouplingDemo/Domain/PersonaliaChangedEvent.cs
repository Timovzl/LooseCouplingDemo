using LooseCouplingDemo.Common.Events;

namespace LooseCouplingDemo.Domain;

/// <summary>
/// A domain event indicating that a set of personalia was changed in the source that we obtained it from.
/// </summary>
public record class PersonaliaChangedEvent() : Event
{
	public required string PersonId { get; init; }
	public required string ModificationAction { get; init; }
}

// We are conceptually aware that the purpose of an event is to be handled
// As such, it makes sense to include a SPECIFIC handler abstraction
public interface IPersonaliaChangedEventHandler : IEventHandler<PersonaliaChangedEvent>;
