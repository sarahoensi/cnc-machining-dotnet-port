using Cnc.Domain.Finishing;

namespace Cnc.Application.Finishing;

public interface IFinishingExecutionStore
{
    void Save(FinishingExecution execution);
    FinishingExecution? Get(Guid id);
}
