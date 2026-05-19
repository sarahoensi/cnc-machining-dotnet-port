namespace Cnc.Domain.Finishing;

public sealed class FinishingExecution
{
    private readonly List<FinishingStep> _steps = new();

    public Guid Id { get; } = Guid.NewGuid();
    public IReadOnlyList<FinishingStep> Steps => _steps;

    public FinishingExecution(IEnumerable<FinishingStep> steps)
    {
        var stepList = steps.ToList();

        if (stepList.Count == 0)
            throw new ArgumentException("Execution must contain at least one step.");

        _steps.AddRange(stepList);
    }

    public void RegisterMeasurement(int stepNumber, Diameter measuredDiameter)
    {
        var step = _steps.SingleOrDefault(s => s.StepNumber == stepNumber);

        if (step is null)
            throw new InvalidOperationException("Step does not exist.");

        var firstOpenStep = _steps.FirstOrDefault(s => !s.IsLocked);

        if (firstOpenStep is null)
            throw new InvalidOperationException("Execution is already completed.");

        if (step.StepNumber != firstOpenStep.StepNumber)
            throw new InvalidOperationException("Measurements must be registered in order.");

        step.RegisterMeasurement(measuredDiameter);
    }
}