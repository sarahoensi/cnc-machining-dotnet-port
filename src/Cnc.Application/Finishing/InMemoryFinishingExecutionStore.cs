using System.Collections.Concurrent;
using Cnc.Domain.Finishing;

namespace Cnc.Application.Finishing;

public sealed class InMemoryFinishingExecutionStore : IFinishingExecutionStore
{
    private readonly ConcurrentDictionary<Guid, FinishingExecution> _executions = new();

    public void Save(FinishingExecution execution)
    {
        _executions[execution.Id] = execution;
    }

    public FinishingExecution? Get(Guid id)
    {
        _executions.TryGetValue(id, out var execution);
        return execution;
    }
}
