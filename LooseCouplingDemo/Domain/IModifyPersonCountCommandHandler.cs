namespace LooseCouplingDemo.Domain;

/// <summary>
/// Handler for the command to store the number of persons.
/// </summary>
public interface IModifyPersonCountCommandHandler
{
	// Note how the command itself is implicitly declared by the method signature: the input is a delta count, the output is void (well Task, because async)
	Task HandleAsync(int deltaCount, CancellationToken cancellationToken);
}

// An explicit command is optional, but perhaps needlessly verbose :)
//public class SyncEntityCommand
//{
//	public required string DeltaCount { get; init; }
//}
