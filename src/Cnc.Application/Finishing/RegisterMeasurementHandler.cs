using Cnc.Domain.Finishing;

namespace Cnc.Application.Finishing;

public sealed class RegisterMeasurementHandler
{
    private readonly IFinishingExecutionStore _store;

    public RegisterMeasurementHandler(IFinishingExecutionStore store)
    {
        _store = store;
    }

    public FinishingExecution Handle(Guid executionId, RegisterMeasurementRequest request)
    {
        var execution = _store.Get(executionId);

        if (execution is null)
            throw new KeyNotFoundException("Execution does not exist.");

        execution.RegisterMeasurement(
            request.StepNumber,
            new Diameter(request.MeasuredDiameterMm)
        );

        _store.Save(execution);

        return execution;
    }
}
