using Cnc.Application.Finishing;
using Cnc.Domain.Finishing;

namespace Cnc.Tests.Unit.Finishing;

public class RegisterMeasurementHandlerTests
{
    [Fact]
    public void Handle_RegistersMeasurementSuccessfully()
    {
        var store = new InMemoryFinishingExecutionStore();
        var execution = CreateExecution();
        store.Save(execution);

        var handler = new RegisterMeasurementHandler(store);

        var updated = handler.Handle(
            execution.Id,
            new RegisterMeasurementRequest(1, 49.75));

        Assert.Equal(49.75, updated.Steps[0].MeasuredDiameter!.Value);
    }

    [Fact]
    public void Handle_LocksStepAfterMeasurement()
    {
        var store = new InMemoryFinishingExecutionStore();
        var execution = CreateExecution();
        store.Save(execution);

        var handler = new RegisterMeasurementHandler(store);

        var updated = handler.Handle(
            execution.Id,
            new RegisterMeasurementRequest(1, 49.75));

        Assert.True(updated.Steps[0].IsLocked);
    }

    [Fact]
    public void Handle_ThrowsWhenRegisteringOutOfOrder()
    {
        var store = new InMemoryFinishingExecutionStore();
        var execution = CreateExecution();
        store.Save(execution);

        var handler = new RegisterMeasurementHandler(store);

        Assert.Throws<InvalidOperationException>(() =>
            handler.Handle(execution.Id, new RegisterMeasurementRequest(2, 49.60)));
    }

    [Fact]
    public void Handle_ThrowsWhenExecutionDoesNotExist()
    {
        var store = new InMemoryFinishingExecutionStore();
        var handler = new RegisterMeasurementHandler(store);

        Assert.Throws<KeyNotFoundException>(() =>
            handler.Handle(Guid.NewGuid(), new RegisterMeasurementRequest(1, 49.70)));
    }

    [Fact]
    public void Handle_ThrowsWhenStepDoesNotExist()
    {
        var store = new InMemoryFinishingExecutionStore();
        var execution = CreateExecution();
        store.Save(execution);

        var handler = new RegisterMeasurementHandler(store);

        Assert.Throws<InvalidOperationException>(() =>
            handler.Handle(execution.Id, new RegisterMeasurementRequest(99, 49.70)));
    }

    private static FinishingExecution CreateExecution()
    {
        return new FinishingExecution(FinishingMode.OuterDiameter, [
            new FinishingStep(1, new Diameter(49.8)),
            new FinishingStep(2, new Diameter(49.6))
        ]);
    }
}
