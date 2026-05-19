using Cnc.Domain.Finishing;

namespace Cnc.Tests.Unit;

public class FinishingStepTests
{
    [Fact]
    public void RegisterMeasurement_LocksStep()
    {
        var step = new FinishingStep(1, new Diameter(49.8));

        step.RegisterMeasurement(new Diameter(49.75));

        Assert.True(step.IsLocked);
        Assert.Equal(49.75, step.MeasuredDiameter!.Value);
    }

    [Fact]
    public void LockedStep_CannotBeChanged()
    {
        var step = new FinishingStep(1, new Diameter(49.8));

        step.RegisterMeasurement(new Diameter(49.75));

        Assert.Throws<InvalidOperationException>(() =>
            step.RegisterMeasurement(new Diameter(49.7)));
    }
}