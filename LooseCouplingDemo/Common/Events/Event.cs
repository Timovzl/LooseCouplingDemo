namespace LooseCouplingDemo.Common.Events;

public abstract record class Event
{
	public required string EventId { get; init; }
}
