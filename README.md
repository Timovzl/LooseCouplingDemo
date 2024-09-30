# Loose Coupling Demo

A demo showing how to use loose coupling without losing IDE navigability.

## Introduction

Loose coupling can have greatly aid in the maintainability of a solution.
For example, a feature can end in the publishing of an event, without caring whether zero, one, or several other features respond to it.
Similarly, a policy in the domain model can invoke a command, without having any knowledge of _how_ that result is achieved.

The usual downside of loose coupling is that developers can no longer navigate sequentially through what happens next using the IDE's features of "go to symbol" and "go to implementation".

This demo shows how the navigability property can be maintained under loose coupling.

## Events & Commands

When publishing events or issuing commands using a mediator-like pattern, we only concern ourselves with a data-carrying object, but not with any handlers.
This is what terminates the navigable chain.

Often, the handler or handlers implement only some generic abstraction, such as `IEventHandler<T>` or `ICommandHandler<T>`.

To establish navigability, introduce a _specific_ abstraction.
For example, imagine that the `SourceEntityChangedEvent` is handled by the `AnnounceSourceDataChangesPolicy`.
Rather than implementing the generic `IEventHandler<SourceEntityChangedEvent>` directly, have the policy implement `ISourceEntityChangedEventHandler`, which implements the generic interface.

Define `ISourceEntityChangedEventHandler` in the same file as the event.
When navigating to the event, developers see the handler interface right next to it and can proceed to the handler implementation.

Note that having `ISourceEntityChangedEventHandler` next to `SourceEntityChangedEvent` makes sense:
The purpose of the event is to be handled. The presence of a handler abstraction is a natural expression of that purpose.

The same principle applies to commands and their handlers.

## Simpler Commands

As an optional simplicifation, it is possible to model commands in a simpler way.

The common model is to define custom commands, while the handler method has a fixed shape, such as `ExecuteAsync<TCommand>(TCommand, CancellationToken)`.
This is expressing the command in terms of the command object.

It is worth noting that the command can also be expressed in terms of the handler.
Consider this interface:

```cs
public interface ISyncEntityCommandHandler
{
	Task HandleAsync(string delta, CancellationToken cancellationToken);
}
```

Where the purpose of the command used to be expressed through the command class' name, the interface expresses it through its own name.
And where the input of the command used to be expressed through the command class' properties, the interface expresses it through its method parameters.

For the purpose of navigability, we are defining the specific command handler anyway.
Since its name and method signature form a complete definition of the command, we can save code by forgoing the command class.

For complex inputs to commands, the method can take complex objects as parameters.
In Domain-Driven Design (DDD), there are often value objects that lend themselves perfectly to the situation.
Alternatively, nothing prevents us from using custom command classes after all.

## Queries

When working with both commands and queries (such as in CQRS), the principles that apply to commands apply to queries as well.

## Vertical Slice Architecture

In Vertical Slice Architecture, it is sometimes possible to go even further for commands and queries.
Rather than placing the handler _abstraction_ in the same file as a the command or query, its _implementation_ can even be placed there.
