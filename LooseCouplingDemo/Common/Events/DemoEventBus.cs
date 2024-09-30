using System.Collections.Immutable;

namespace LooseCouplingDemo.Common.Events;

public class DemoEventBus(
	IServiceProvider serviceProvider)
	: IEventBus
{
	public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
		where TEvent : Event
	{
		var eventPublisher = serviceProvider.GetRequiredService<EventPublisher<TEvent>>();
		return eventPublisher.PublishAsync(@event, cancellationToken);
	}
}

internal class EventPublisher<TEvent>(
	IEnumerable<IEventHandler<TEvent>> handlers)
	where TEvent : Event
{
	private ImmutableArray<IEventHandler<TEvent>> Handlers { get; } = handlers.ToImmutableArray();

	public Task PublishAsync(TEvent @event, CancellationToken cancellationToken)
	{
		var tasks = this.Handlers.Select(handler => handler.HandleAsync(@event, cancellationToken));
		return Task.WhenAll(tasks);
	}
}
