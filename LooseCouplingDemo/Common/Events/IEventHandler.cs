namespace LooseCouplingDemo.Common.Events;

public interface IEventHandler<TEvent>
	where TEvent : Event
{
	Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}

public interface IEventHandler;
