namespace LooseCouplingDemo.Common.Events;

public interface IEventBus
{
	Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
		where TEvent : Event;
}
