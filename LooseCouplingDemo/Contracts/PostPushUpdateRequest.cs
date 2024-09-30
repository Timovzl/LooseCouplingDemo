namespace LooseCouplingDemo.Contracts;

public record class PostPushUpdateRequest
{
	public required string Id { get; init; }
	public required string ModificationAction { get; init; }
	public required string Delta { get; init; }
}
