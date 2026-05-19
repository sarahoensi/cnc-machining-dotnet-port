using Cnc.Domain.Finishing;

namespace Cnc.Tests.Unit;

public class FinishingPlanGeneratorTests
{
    [Fact]
    public void Generate_CreatesExpectedNumberOfSteps()
    {
        var execution = FinishingPlanGenerator.Generate(
            new Diameter(50.0),
            new Diameter(49.0),
            4);

        Assert.Equal(4, execution.Steps.Count);
    }

    [Fact]
    public void Generate_CalculatesPlannedDiameters()
    {
        var execution = FinishingPlanGenerator.Generate(
            new Diameter(50.0),
            new Diameter(49.0),
            4);

        Assert.Equal(49.75, execution.Steps[0].PlannedDiameter.Value);
        Assert.Equal(49.50, execution.Steps[1].PlannedDiameter.Value);
        Assert.Equal(49.25, execution.Steps[2].PlannedDiameter.Value);
        Assert.Equal(49.00, execution.Steps[3].PlannedDiameter.Value);
    }

    [Fact]
    public void Generate_ThrowsWhenStartIsNotLargerThanTarget()
    {
        Assert.Throws<ArgumentException>(() =>
            FinishingPlanGenerator.Generate(
                new Diameter(49.0),
                new Diameter(50.0),
                4));
    }
}