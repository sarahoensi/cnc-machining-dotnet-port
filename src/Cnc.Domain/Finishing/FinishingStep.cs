namespace Cnc.Domain.Finishing;

public sealed class FinishingStep
{
    public int StepNumber { get; }
    public Diameter PlannedDiameter { get; }
    public Diameter? MeasuredDiameter { get; private set; }
    public bool IsLocked { get; private set; }

    public FinishingStep(int stepNumber, Diameter plannedDiameter)
    {
        if (stepNumber < 1)
            throw new ArgumentException("Step number must be 1 or greater.");

        StepNumber = stepNumber;
        PlannedDiameter = plannedDiameter;
    }

    public void RegisterMeasurement(Diameter measuredDiameter)
    {
        if (IsLocked)
            throw new InvalidOperationException("Locked steps cannot be changed.");

        MeasuredDiameter = measuredDiameter;
        IsLocked = true;
    }
}