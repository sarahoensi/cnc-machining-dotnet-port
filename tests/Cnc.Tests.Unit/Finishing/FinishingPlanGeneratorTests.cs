using Cnc.Domain.Finishing;

namespace Cnc.Tests.Unit;

public class FinishingPlanGeneratorTests
{
    [Fact]
    public void Generate_OuterDiameter_CreatesValidPlan()
    {
        var execution = FinishingPlanGenerator.Generate(
            FinishingMode.OuterDiameter,
            new Diameter(50.0),
            new Diameter(49.0),
            4);

        Assert.Equal(FinishingMode.OuterDiameter, execution.Mode);
        Assert.Equal(4, execution.Steps.Count);
        Assert.Equal(49.75, execution.Steps[0].PlannedDiameter.Value);
        Assert.Equal(49.50, execution.Steps[1].PlannedDiameter.Value);
        Assert.Equal(49.25, execution.Steps[2].PlannedDiameter.Value);
        Assert.Equal(49.00, execution.Steps[3].PlannedDiameter.Value);
    }

    [Fact]
    public void Generate_InnerDiameter_CreatesValidPlan()
    {
        var execution = FinishingPlanGenerator.Generate(
            FinishingMode.InnerDiameter,
            new Diameter(49.0),
            new Diameter(50.0),
            4);

        Assert.Equal(FinishingMode.InnerDiameter, execution.Mode);
        Assert.Equal(4, execution.Steps.Count);
        Assert.Equal(49.25, execution.Steps[0].PlannedDiameter.Value);
        Assert.Equal(49.50, execution.Steps[1].PlannedDiameter.Value);
        Assert.Equal(49.75, execution.Steps[2].PlannedDiameter.Value);
        Assert.Equal(50.00, execution.Steps[3].PlannedDiameter.Value);
    }

    [Fact]
    public void Generate_OuterDiameter_ThrowsForInvalidDirection()
    {
        Assert.Throws<ArgumentException>(() =>
            FinishingPlanGenerator.Generate(
                FinishingMode.OuterDiameter,
                new Diameter(49.0),
                new Diameter(50.0),
                4));
    }

    [Fact]
    public void Generate_InnerDiameter_ThrowsForInvalidDirection()
    {
        Assert.Throws<ArgumentException>(() =>
            FinishingPlanGenerator.Generate(
                FinishingMode.InnerDiameter,
                new Diameter(50.0),
                new Diameter(49.0),
                4));
    }
}
