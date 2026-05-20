namespace Cnc.Domain.Finishing;

public static class FinishingPlanGenerator
{
    public static FinishingExecution Generate(
        FinishingMode mode,
        Diameter startDiameter,
        Diameter targetDiameter,
        int numberOfCuts)
    {
        if (numberOfCuts < 1)
            throw new ArgumentException("Number of cuts must be at least 1.");

        var start = startDiameter.Value;
        var target = targetDiameter.Value;

        switch (mode)
        {
            case FinishingMode.OuterDiameter when start <= target:
                throw new ArgumentException(
                    "OuterDiameter requires start diameter to be larger than target diameter.");
            case FinishingMode.InnerDiameter when start >= target:
                throw new ArgumentException(
                    "InnerDiameter requires start diameter to be smaller than target diameter.");
        }

        var signedDelta = mode switch
        {
            FinishingMode.OuterDiameter => (target - start) / numberOfCuts,
            FinishingMode.InnerDiameter => (target - start) / numberOfCuts,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unknown finishing mode.")
        };

        var steps = new List<FinishingStep>();

        for (var i = 1; i <= numberOfCuts; i++)
        {
            var plannedDiameter = start + signedDelta * i;

            steps.Add(new FinishingStep(
                i,
                new Diameter(plannedDiameter)
            ));
        }

        return new FinishingExecution(mode, steps);
    }
}
