namespace Cnc.Domain.Finishing;

public static class FinishingPlanGenerator
{
    public static FinishingExecution Generate(
        Diameter startDiameter,
        Diameter targetDiameter,
        int numberOfCuts)
    {
        if (numberOfCuts < 1)
            throw new ArgumentException("Number of cuts must be at least 1.");

        if (startDiameter.Value <= targetDiameter.Value)
            throw new ArgumentException("Start diameter must be larger than target diameter.");

        var totalToRemove = startDiameter.Value - targetDiameter.Value;
        var cutSize = totalToRemove / numberOfCuts;

        var steps = new List<FinishingStep>();

        for (var i = 1; i <= numberOfCuts; i++)
        {
            var plannedDiameter = startDiameter.Value - cutSize * i;

            steps.Add(new FinishingStep(
                i,
                new Diameter(plannedDiameter)
            ));
        }

        return new FinishingExecution(steps);
    }
}