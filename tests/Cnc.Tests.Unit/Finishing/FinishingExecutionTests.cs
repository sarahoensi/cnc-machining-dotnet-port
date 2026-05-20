using Cnc.Domain.Finishing;

namespace Cnc.Tests.Unit;

public class FinishingExecutionTests
{
    [Fact]
    public void RegisterMeasurement_LocksFirstOpenStep()
    {
        var execution = new FinishingExecution(FinishingMode.OuterDiameter, [
            new FinishingStep(1, new Diameter(49.8)),
            new FinishingStep(2, new Diameter(49.6))
        ]);

        execution.RegisterMeasurement(1, new Diameter(49.75));

        Assert.True(execution.Steps[0].IsLocked);
        Assert.False(execution.Steps[1].IsLocked);
    }

    [Fact]
    public void CannotRegisterMeasurementOutOfOrder()
    {
        var execution = new FinishingExecution(FinishingMode.OuterDiameter, [
            new FinishingStep(1, new Diameter(49.8)),
            new FinishingStep(2, new Diameter(49.6))
        ]);

        Assert.Throws<InvalidOperationException>(() =>
            execution.RegisterMeasurement(2, new Diameter(49.55)));
    }

    [Fact]
    public void CannotRegisterMeasurementForMissingStep()
    {
        var execution = new FinishingExecution(FinishingMode.OuterDiameter, [
            new FinishingStep(1, new Diameter(49.8))
        ]);

        Assert.Throws<InvalidOperationException>(() =>
            execution.RegisterMeasurement(99, new Diameter(49.7)));
    }
}
